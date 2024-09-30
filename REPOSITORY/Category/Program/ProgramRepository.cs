using System.Data;
using System.Net;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Dapper;
using DTO.Base;
using DTO.Category.Program.Models;
using DTO.Category.Program.Dtos;
using REPOSITORY.Common;
using Microsoft.AspNetCore.Http;

namespace REPOSITORY.Category.Program;

[RegisterClassAsTransient]
public class ProgramRepository(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IProgramRepository
{
    public async Task<bool> DeLeteList(DeleteListRequest request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();
            foreach (var id in request.Ids)
            {
                var entity = await unitOfWork.GetRepository<DAL.Entities.Program>().GetByIdAsync(id);

                if (entity != null)
                {
                    entity.IsDeleted = true;
                    entity.DeletedAt = DateTime.Now;
                    entity.DeletedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;

                    await unitOfWork.GetRepository<DAL.Entities.Program>().UpdateAsync(entity);
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
        var result = unitOfWork.GetRepository<DAL.Entities.Program>()
            .GetAll(x => !x.IsDeleted && x.IsActived)
            .OrderBy(x => x.Name)
            .ToList();

        List<ComboboxModel> response = result.Select(x => new ComboboxModel
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).OrderBy(x => x.Sort).ToList();

        return response;

    }

    public async Task<ProgramModel> GetById(GetByIdRequest request)
    {
        var data = await unitOfWork.GetRepository<DAL.Entities.Program>().GetByIdAsync(request.Id);
        if (data == null)
        {
            throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
        }

        var result = mapper.Map<ProgramModel>(data);
        return result;
    }

    public async Task<ProgramDto> GetByPost(GetByIdRequest request)
    {

            var result = new ProgramDto();
            var data = await unitOfWork.GetRepository<DAL.Entities.Program>().GetByIdAsync(request.Id);

            if (data == null)
            {
                result.Id = Guid.NewGuid();
                result.IsEdit = false;
            }
            else
            {
                result = mapper.Map<ProgramDto>(data);
                result.IsEdit = true;
            }

            return result;
    }


    public async Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@iTextSearch", request.Search, DbType.String);
        parameters.Add("@iPageIndex", request.Offset / request.Limit, DbType.Int32);
        parameters.Add("@iRowsPerPage", request.Limit, DbType.Int32);
        parameters.Add("@oTotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

        var result = await unitOfWork.GetRepository<ProgramModel>().ExecWithStoreProcedure("sp_Category_Program_GetListPaging", parameters);

        var totalRow = parameters.Get<long>("@oTotalRow");
        var response = new GetListPagingResponse()
        {
            PageIndex = request.Offset,
            Data = result,
            TotalRow = Convert.ToInt32(totalRow)
        };
        return response;
    }

    public async Task<bool> Insert(ProgramDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.Program>().Find(x =>
                !x.IsDeleted &&
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }

            var entity = mapper.Map<DAL.Entities.Program>(request);
            entity.CreatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;
            entity.UpdatedAt = DateTime.Now;

            await unitOfWork.GetRepository<DAL.Entities.Program>().AddAsync(entity);

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

    public async Task<bool> Update(ProgramDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.Program>().Find(x =>
                !x.IsDeleted &&
                x.Id != request.Id &&
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }

            var data = await unitOfWork.GetRepository<DAL.Entities.Program>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
            }
            var entity = mapper.Map(request, data);

            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;

            await unitOfWork.GetRepository<DAL.Entities.Program>().UpdateAsync(entity);

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