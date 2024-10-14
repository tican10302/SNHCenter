using System.Data;
using System.Net;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Dapper;
using DTO.Base;
using DTO.Category.Program.Dtos;
using DTO.Category.Program.Models;
using DTO.Management.CourseTemplate.Dtos;
using DTO.Management.CourseTemplate.Models;
using Microsoft.AspNetCore.Http;
using REPOSITORY.Common;

namespace REPOSITORY.Management.CourseTemplate;

[RegisterClassAsTransient]
public class CourseTemplateRepository(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : ICourseTemplateRepository
{
    public async Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@iTextSearch", request.Search, DbType.String);
        parameters.Add("@iPageIndex", request.Offset / request.Limit, DbType.Int32);
        parameters.Add("@iRowsPerPage", request.Limit, DbType.Int32);
        parameters.Add("@oTotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

        var result = await unitOfWork.GetRepository<CourseTemplateModel>().ExecWithStoreProcedure("sp_Management_CourseTemplate_GetListPaging", parameters);

        var totalRow = parameters.Get<long>("@oTotalRow");
        var response = new GetListPagingResponse()
        {
            PageIndex = request.Offset,
            Data = result,
            TotalRow = Convert.ToInt32(totalRow)
        };
        return response;
    }
    
    public async Task<CourseTemplateModel> GetById(GetByIdRequest request)
    {
        var data = await unitOfWork.GetRepository<DAL.Entities.CourseTemplate>().GetByIdAsync(request.Id);
        if (data == null)
        {
            throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
        }

        var result = mapper.Map<CourseTemplateModel>(data);
        return result;
    }
    
    public async Task<CourseTemplateDto> GetByPost(GetByIdRequest request)
    {

        var result = new CourseTemplateDto();
        var data = await unitOfWork.GetRepository<DAL.Entities.CourseTemplate>().GetByIdAsync(request.Id);

        if (data == null)
        {
            result.Id = Guid.NewGuid();
            result.IsEdit = false;
        }
        else
        {
            result = mapper.Map<CourseTemplateDto>(data);
            result.IsEdit = true;
        }

        return result;
    }
    
    public async Task<bool> Insert(CourseTemplateDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.CourseTemplate>().Find(x =>
                !x.IsDeleted &&
                x.LevelId == request.LevelId);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }
            
            

            var entity = mapper.Map<DAL.Entities.CourseTemplate>(request);
            entity.CreatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;
            entity.UpdatedAt = DateTime.Now;

            await unitOfWork.GetRepository<DAL.Entities.CourseTemplate>().AddAsync(entity);

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
    
    public async Task<bool> Update(CourseTemplateDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.CourseTemplate>().Find(x =>
                !x.IsDeleted &&
                x.Id != request.Id &&
                x.LevelId == request.LevelId);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }

            var data = await unitOfWork.GetRepository<DAL.Entities.CourseTemplate>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
            }
            var entity = mapper.Map(request, data);

            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;

            await unitOfWork.GetRepository<DAL.Entities.CourseTemplate>().UpdateAsync(entity);

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
    
    public async Task<bool> DeLeteList(DeleteListRequest request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();
            foreach (var id in request.Ids)
            {
                var entity = await unitOfWork.GetRepository<DAL.Entities.CourseTemplate>().GetByIdAsync(id);

                if (entity != null)
                {
                    entity.IsDeleted = true;
                    entity.DeletedAt = DateTime.Now;
                    entity.DeletedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;

                    await unitOfWork.GetRepository<DAL.Entities.CourseTemplate>().UpdateAsync(entity);
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
}