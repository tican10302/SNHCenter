using DTO.Base;
using DTO.System.Account.Dtos;
using DTO.System.Account.Models;

namespace REPOSITORY.System.Account;

public interface IAccountRepository
{
    Task<BaseResponse<AccountModel>> Register(RegisterDto request);
    Task<BaseResponse<AccountPermissionModel>> Login(AccountDto request);
    Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request);
    Task<BaseResponse<UserDto>> GetByPost(GetByIdRequest request);
    Task<BaseResponse<UserModel>> GetById(GetByIdRequest request);
    Task<BaseResponse<UserModel>> Insert(UserDto request);
    Task<BaseResponse<UserModel>> Update(UserDto request);
    Task<BaseResponse<string>> DeLeteList(DeleteListRequest request);
    Task<BaseResponse<List<PermissionModel>>> GetPermission(GetByIdRequest request);
}