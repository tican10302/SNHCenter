using DTO.Base;
using DTO.System.Account.Dtos;
using DTO.System.Account.Models;

namespace REPOSITORY.System.Account;

public interface IAccountRepository
{
    Task<AccountModel> Register(RegisterDto request);
    Task<AccountPermissionModel> Login(AccountDto request);
    Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request);
    Task<UserDto> GetByPost(GetByIdRequest request);
    Task<UserModel> GetById(GetByIdRequest request);
    Task<bool> Insert(UserDto request);
    Task<bool> Update(UserDto request);
    Task<bool> DeLeteList(DeleteListRequest request);
    Task<List<PermissionModel>> GetPermission(GetByIdRequest request);
}