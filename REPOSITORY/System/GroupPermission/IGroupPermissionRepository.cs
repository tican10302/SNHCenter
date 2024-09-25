using DTO.Base;
using DTO.System.Account.Models;
using DTO.System.GroupPermission.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace REPOSITORY.System.GroupPermission;

public interface IGroupPermissionRepository
{
    Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
    List<GroupPermissionModel> GetAll();
    Task<GroupPermissionModel> GetById(GetByIdRequest request);
    Task<GroupPermissionDto> GetByPost(GetByIdRequest request);
    Task<bool> Insert(GroupPermissionDto request);
    Task<bool> Update(GroupPermissionDto request);
    List<SelectListItem> GetAllForCombobox();
}