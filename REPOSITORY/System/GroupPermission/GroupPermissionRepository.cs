using System.Data;
using System.Net;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using Dapper;
using DTO.Base;
using DTO.System.Account.Models;
using DTO.System.GroupPermission.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;
using REPOSITORY.Common;

namespace REPOSITORY.System.GroupPermission;

[RegisterClassAsTransient]
public class GroupPermissionRepository(IUnitOfWork unitOfWork, IMapper mapper) : IGroupPermissionRepository
{
    public async Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@iTextSearch", request.Search, DbType.String);
        parameters.Add("@iPageIndex", request.Offset, DbType.Int32);
        parameters.Add("@iRowsPerPage", request.Limit, DbType.Int32);
        parameters.Add("@oTotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

        var result = await unitOfWork.GetRepository<GroupPermissionModel>().ExecWithStoreProcedure("sp_Sys_GroupPermission_GetListPaging", parameters);


        var totalRow = parameters.Get<long>("@oTotalRow");
        var responseData = new GetListPagingResponse
        {
            PageIndex = request.Offset,
            Data = result,
            TotalRow = Convert.ToInt32(totalRow)
        };
        return responseData;
    }

    public async Task<GroupPermissionModel> GetById(GetByIdRequest request)
    {
        var data = await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().GetByIdAsync(request.Id);
        if (data == null)
        {
            throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
        }

        var result = mapper.Map<GroupPermissionModel>(data);
        return result;
    }

    public async Task<GroupPermissionDto> GetByPost(GetByIdRequest request)
    {
        var result = new GroupPermissionDto();
        var data = await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().GetByIdAsync(request.Id);
        if (data == null)
        {
            result.Id = Guid.NewGuid();
            result.IsEdit = false;
        }
        else
        {
            result = mapper.Map<GroupPermissionDto>(data);
            result.IsEdit = true;
        }
        return result;
    }

    public async Task<bool> Insert(GroupPermissionDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().Find(x => 
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }
            
            var entity = mapper.Map<DAL.Entities.GroupPermission>(request);
            
            await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().AddAsync(entity);
            
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

    public async Task<bool> Update(GroupPermissionDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().Find(x =>
                x.Id != request.Id &&
                x.Name == request.Name);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }

            var data = await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
            }
            var entity = mapper.Map(request, data);
            await unitOfWork.GetRepository<DAL.Entities.GroupPermission>().UpdateAsync(entity);
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

    public List<GroupPermissionModel> GetAll()
    {
        var result = unitOfWork.GetRepository<DAL.Entities.GroupPermission>()
            .GetAll(x => x.IsActived)
            .OrderBy(x => x.Sort)
            .ToList();

        var response = mapper.Map<List<GroupPermissionModel>>(result);
        return response;
    }

    public List<SelectListItem> GetAllForCombobox()
    {
        var result = unitOfWork.GetRepository<DAL.Entities.GroupPermission>()
            .GetAll(x => x.IsActived)
            .OrderBy(x => x.Sort)
            .ToList();

        var response = result.Select(x => new SelectListItem
        {
            Text = x.Name,
            Value = x.Id.ToString()
        }).ToList();
        return response;
    }
}