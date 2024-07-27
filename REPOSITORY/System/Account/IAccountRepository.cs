using DTO.Base;
using DTO.System.Account.Dtos;
using DTO.System.Account.Requests;

namespace REPOSITORY.System.Account;

public interface IAccountRepository
{
    Task<BaseResponse<AccountModel>> Register(RegisterDto request);
    Task<BaseResponse<AccountPermissionModel>> Login(AccountDto request);
}