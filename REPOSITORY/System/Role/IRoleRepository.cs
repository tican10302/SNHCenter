using DTO.Base;
using DTO.System.Role.Dtos;
using DTO.System.Role.Models;

namespace REPOSITORY.System.Role
{
    public interface IRoleRepository
    {
        Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
        Task<RoleModel> GetById(GetByIdRequest request);
        Task<RoleDto> GetByPost(GetByIdRequest request);
        Task<bool> Insert(RoleDto request);
        Task<bool> Update(RoleDto request);
        Task<bool> DeleteList(DeleteListRequest request);
        List<ComboboxModel> GetAllForCombobox();
        Task<List<Role_PermissionModel>> GetListRolePermission(GetRole_PermissionDto request);
        Task<Role_PermissionModel> PostRolePermission(Role_PermissionDto request);
    }
}
