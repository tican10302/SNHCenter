using System.Data;
using System.Net;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Dapper;
using DTO.Base;
using DTO.Management.Parent.Models;
using DTO.Management.Parent.Dtos;
using REPOSITORY.Common;
using Microsoft.AspNetCore.Http;
using REPOSITORY.Management.Parent;

namespace REPOSITORY.Category.Parent;

[RegisterClassAsTransient]
public class ParentRepository(IUnitOfWork unitOfWork, IMapper mapper, IHttpContextAccessor httpContextAccessor) : IParentRepository
{
    public async Task<bool> DeLeteList(DeleteListRequest request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();
            foreach (var id in request.Ids)
            {
                var entity = await unitOfWork.GetRepository<DAL.Entities.Parent>().GetByIdAsync(id);

                if (entity != null)
                {
                    entity.IsDeleted = true;
                    entity.DeletedAt = DateTime.Now;
                    entity.DeletedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;

                    await unitOfWork.GetRepository<DAL.Entities.Parent>().UpdateAsync(entity);
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
        var result = unitOfWork.GetRepository<DAL.Entities.Parent>()
            .GetAll(x => !x.IsDeleted && x.IsActive)
            .OrderBy(x => x.FirstName)
            .ToList();

        List<ComboboxModel> response = result.Select(x => new ComboboxModel
        {
            Text = x.FirstName,
            Value = x.Id.ToString()
        }).OrderBy(x => x.Sort).ToList();

        return response;

    }

    public async Task<ParentModel> GetById(GetByIdRequest request)
    {
        var data = await unitOfWork.GetRepository<DAL.Entities.Parent>().GetByIdAsync(request.Id);
        if (data == null)
        {
            throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
        }

        var result = mapper.Map<ParentModel>(data);
        return result;
    }

    public async Task<ParentDto> GetByPost(GetByIdRequest request)
    {

        var result = new ParentDto();
        var data = await unitOfWork.GetRepository<DAL.Entities.Parent>().GetByIdAsync(request.Id);

        if (data == null)
        {
            result.Id = Guid.NewGuid();
            result.IsEdit = false;
        }
        else
        {
            result = mapper.Map<ParentDto>(data);
            result.IsEdit = true;
        }

        return result;
    }


    public async Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@iProvinceId", request.ProvinceId, DbType.Guid);
        parameters.Add("@iDistrictId", request.DistrictId, DbType.Guid);
        parameters.Add("@iWardId", request.WardId, DbType.Guid);
        parameters.Add("@iTextSearch", request.Search, DbType.String);
        parameters.Add("@iTextSearch", request.Search, DbType.String);
        parameters.Add("@iPageIndex", request.Offset / request.Limit, DbType.Int32);
        parameters.Add("@iRowsPerPage", request.Limit, DbType.Int32);
        parameters.Add("@oTotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

        var result = await unitOfWork.GetRepository<ParentModel>().ExecWithStoreProcedure("sp_Category_Parent_GetListPaging", parameters);

        var totalRow = parameters.Get<long>("@oTotalRow");
        var response = new GetListPagingResponse()
        {
            PageIndex = request.Offset,
            Data = result,
            TotalRow = Convert.ToInt32(totalRow)
        };
        return response;
    }

    public async Task<bool> Insert(ParentDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.Parent>().Find(x =>
                !x.IsDeleted &&
                x.FirstName == request.FirstName);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }

            var entity = mapper.Map<DAL.Entities.Parent>(request);
            entity.CreatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;
            entity.UpdatedAt = DateTime.Now;

            await unitOfWork.GetRepository<DAL.Entities.Parent>().AddAsync(entity);

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

    public async Task<bool> Update(ParentDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.Parent>().Find(x =>
                !x.IsDeleted &&
                x.Id != request.Id &&
                x.FirstName == request.FirstName);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }

            var data = await unitOfWork.GetRepository<DAL.Entities.Parent>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
            }
            var entity = mapper.Map(request, data);

            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = httpContextAccessor.HttpContext?.User.Identity?.Name;

            await unitOfWork.GetRepository<DAL.Entities.Parent>().UpdateAsync(entity);

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