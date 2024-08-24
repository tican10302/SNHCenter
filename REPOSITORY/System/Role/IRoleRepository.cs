using DTO.Base;
using DTO.System.Role.Dtos;
using DTO.System.Role.Models;

namespace REPOSITORY.System.Role
{
    public interface IRoleRepository
    {
        Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request);
        Task<BaseResponse<RoleDto>> GetByPost(GetByIdRequest request);
        Task<BaseResponse<RoleModel>> GetById(GetByIdRequest request);
        Task<BaseResponse<RoleModel>> Insert(RoleDto request);
        Task<BaseResponse<RoleModel>> Update(RoleDto request);
        Task<BaseResponse<string>> DeLeteList(DeleteListRequest request);
        Task<BaseResponse<List<ComboboxModel>>> GetAllForCombobox(GetAllRequest request);
        Task<BaseResponse<List<Role_PermissionModel>>> GetListRolePermission(GetRole_PermissionDto request);
        Task<BaseResponse<Role_PermissionModel>> PostRolePermission(Role_PermissionDto request);
    }
}
