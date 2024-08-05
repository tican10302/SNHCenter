using System.Data;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using DAL.Data;
using DAL.Entities;
using Dapper;
using DTO.Base;
using DTO.System.Account.Dtos;
using DTO.System.Account.Requests;
using Microsoft.Data.SqlClient;
using REPOSITORY.Common;
using Microsoft.EntityFrameworkCore;

namespace REPOSITORY.System.Account;

[RegisterClassAsTransient]
public class AccountRepository(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService, DataContext context) : IAccountRepository
{
    public async Task<BaseResponse<AccountModel>> Register(RegisterDto request)
    {
        var response = new BaseResponse<AccountModel>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();
            
            // 1. Check password
            if (request.Password != null && !request.Password.Equals(request.PasswordConfirm))
            {
                throw new Exception("Password is not equal");
            }

            // 2. Check account exist
            var accountExist = await unitOfWork.GetRepository<DAL.Entities.Account>()
                .Find(x => x.UserName == request.UserName);

            if (accountExist != null)
            {
                throw new Exception("Username is exist");
            }
            
            // 3. Hash password
            var entity = mapper.Map<DAL.Entities.Account>(request);
            entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
            
            // 4. Store to database
            entity.CreatedBy = "admin";
            entity.UpdatedBy = "admin";
            var data = await unitOfWork.GetRepository<DAL.Entities.Account>().AddAsync(entity);
            var account = mapper.Map<AccountModel>(data);
            
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
            
            // 5. Generate JWT Token
            account.Token = tokenService.CreateToken(data);

            response.Data = account;

        }
        catch (Exception e)
        {
            await unitOfWork.RollbackAsync();
            response.Error = true;
            response.Message = e.Message;
        }

        return response;
    }

    public async Task<BaseResponse<AccountPermissionModel>> Login(AccountDto request)
    {
        var response = new BaseResponse<AccountPermissionModel>();

        try
        {
            var data = new AccountPermissionModel();
            var account = await unitOfWork.GetRepository<DAL.Entities.Account>()
                .Find(x => x.UserName.ToLower() == request.UserName.ToLower());
            if (account == null)
            {
                throw new Exception("Username or password is not exist");
            }
            else
            {
                var checkPassword = BCrypt.Net.BCrypt.Verify(request.Password, account.PasswordHash);
                if (!checkPassword)
                {
                    throw new Exception("Username or password is not exist");
                }

                var userInfo = context.Users.Include(x => x.Role).FirstOrDefault(x => x.Account.Id == account.Id);

                if(userInfo == null)
                {
                    throw new Exception("User has not been activated yet");
                }
                var role = context.Roles.FirstOrDefault(x => x.Id == userInfo.Role.Id);

                if (role == null)
                {
                    throw new Exception("User has not been authorized");
                }

                data.Account = mapper.Map<AccountModel>(account);
                data.Account.FirstName = userInfo.FirstName;
                data.Account.LastName = userInfo.LastName;
                data.Account.Role = role.Name;
                data.Account.Avatar = string.IsNullOrWhiteSpace(account.Avatar) ? "img/avatar/avatar-default.png" : account.Avatar;
                data.Account.Token = tokenService.CreateToken(account);

                var parameters = new DynamicParameters();
                parameters.Add("@iAccountId", account.Id);
                var permissions = await unitOfWork.GetRepository<PermissionModel>().ExecWithStoreProcedure("sp_Sys_Account_GetPermission", parameters);

                data.Permission = mapper.Map<List<PermissionModel>>(permissions);

                response.Data = data;
            }
        }
        catch (Exception e)
        {
            response.Error = true;
            response.Message = e.Message;
        }

        return response;
    }
    
    //GET PERMISSION
    public async Task<BaseResponse<List<PermissionModel>>> GetPermission(GetByIdRequest request)
    {
        var response = new BaseResponse<List<PermissionModel>>();
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iAccountId", request.Id);

            response.Data = await unitOfWork.GetRepository<PermissionModel>().ExecWithStoreProcedure("sp_Sys_Account_GetPermission", parameters);
        }
        catch (Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }
}