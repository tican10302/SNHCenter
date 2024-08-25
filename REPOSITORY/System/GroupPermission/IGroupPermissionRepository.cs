using DTO.Base;
using DTO.System.Account.Models;
using DTO.System.GroupPermission.Dtos;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace REPOSITORY.System.GroupPermission;

public interface IGroupPermissionRepository
{
    Task<BaseResponse<List<GroupPermissionModel>>> GetList(GetAllRequest request);
    Task<BaseResponse<GroupPermissionModel>> GetById(GetByIdRequest request);
    Task<BaseResponse<GroupPermissionDto>> GetByPost(GetByIdRequest request);
    Task<BaseResponse<GroupPermissionModel>> Insert(GroupPermissionDto request);
    Task<BaseResponse<GroupPermissionModel>> Update(GroupPermissionDto request);
    Task<BaseResponse<List<GroupPermissionModel>>> GetAll(GetAllRequest request);
    Task<BaseResponse<List<SelectListItem>>> GetAllForCombobox(GetAllRequest request);
}