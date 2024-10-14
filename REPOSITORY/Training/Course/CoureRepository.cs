using System.Data;
using System.Net;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Dapper;
using DTO.Base;
using DTO.Training.Course.Models;
using DTO.Training.Course.Dtos;
using REPOSITORY.Common;
using Microsoft.AspNetCore.Http;

namespace REPOSITORY.Training.Course;

[RegisterClassAsTransient]
public class CourseRepository(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : ICourseRepository
{
    public async Task<bool> DeLeteList(DeleteListRequest request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();
            foreach (var id in request.Ids)
            {
                var entity = await unitOfWork.GetRepository<DAL.Entities.Course>().GetByIdAsync(id);

                if (entity != null)
                {
                    entity.IsDeleted = true;
                    entity.DeletedAt = DateTime.Now;
                    entity.DeletedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;

                    await unitOfWork.GetRepository<DAL.Entities.Course>().UpdateAsync(entity);
                }

                await unitOfWork.SaveChangesAsync();
            }
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }

        return true;
    }

    public List<ComboboxModel> GetAllForCombobox()
    {
        var result = unitOfWork.GetRepository<DAL.Entities.Course>()
            .GetAll(x => !x.IsDeleted && x.IsActive)
            .OrderBy(x => x.Name)
            .ToList();

        List<ComboboxModel> response = result.Select(x => new ComboboxModel
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).OrderBy(x => x.Sort).ToList();

        return response;

    }

    public async Task<CourseModel> GetById(GetByIdRequest request)
    {
        var data = await unitOfWork.GetRepository<DAL.Entities.Course>().GetByIdAsync(request.Id);
        if (data == null)
        {
            throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
        }

        var result = mapper.Map<CourseModel>(data);
        return result;
    }

    public async Task<CourseDto> GetByPost(GetByIdRequest request)
    {

        var result = new CourseDto();
        var data = await unitOfWork.GetRepository<DAL.Entities.Course>().GetByIdAsync(request.Id);

        if (data == null)
        {
            result.Id = Guid.NewGuid();
            result.IsEdit = false;
        }
        else
        {
            result = mapper.Map<CourseDto>(data);
            result.IsEdit = true;
        }

        return result;
    }


    public async Task<GetListPagingResponse> GetListPaging(CourseGetListDto request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@iLevelId", request.LevelId, DbType.Guid);
        parameters.Add("@iShiftId", request.ShiftId, DbType.Guid);
        parameters.Add("@iTextSearch", request.Search, DbType.String);
        parameters.Add("@iPageIndex", request.Offset / request.Limit, DbType.Int32);
        parameters.Add("@iRowsPerPage", request.Limit, DbType.Int32);
        parameters.Add("@oTotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

        var result = await unitOfWork.GetRepository<CourseModel>().ExecWithStoreProcedure("sp_Training_Course_GetListPaging", parameters);

        var totalRow = parameters.Get<long>("@oTotalRow");
        var response = new GetListPagingResponse()
        {
            PageIndex = request.Offset,
            Data = result,
            TotalRow = Convert.ToInt32(totalRow)
        };
        return response;
    }

    public async Task<bool> Insert(CourseDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.Course>().Find(x =>
                !x.IsDeleted &&
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }

            var entity = mapper.Map<DAL.Entities.Course>(request);
            entity.CreatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;
            entity.UpdatedAt = DateTime.Now;

            await unitOfWork.GetRepository<DAL.Entities.Course>().AddAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }

        return true;
    }

    public async Task<bool> Update(CourseDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.Course>().Find(x =>
                !x.IsDeleted &&
                x.Id != request.Id &&
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }

            var data = await unitOfWork.GetRepository<DAL.Entities.Course>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
            }
            var entity = mapper.Map(request, data);

            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;

            await unitOfWork.GetRepository<DAL.Entities.Course>().UpdateAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }

        return true;
    }
}