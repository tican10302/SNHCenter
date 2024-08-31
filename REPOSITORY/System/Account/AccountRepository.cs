using System.Data;
using AutoDependencyRegistration.Attributes;
using AutoMapper;
using DAL.Data;
using DAL.Entities;
using Dapper;
using DTO.Base;
using DTO.System.Account.Dtos;
using DTO.System.Account.Models;
using DTO.System.Menu.Models;
using REPOSITORY.Common;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Http;
using REPOSITORY.System.GroupPermission;

namespace REPOSITORY.System.Account;

[RegisterClassAsTransient]
public class AccountRepository(IUnitOfWork unitOfWork, 
    IMapper mapper, 
    ITokenService tokenService, 
    IConfiguration config, 
    DataContext context, 
    IHttpContextAccessor contextAccessor,
    IGroupPermissionRepository groupPermissionRepository) : IAccountRepository
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
            entity.CreatedBy = contextAccessor.HttpContext.User.Identity.Name;
            entity.UpdatedBy = contextAccessor.HttpContext.User.Identity.Name;
            var data = await unitOfWork.GetRepository<DAL.Entities.Account>().AddAsync(entity);
            var account = mapper.Map<AccountModel>(data);
            
            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();
            
            // 5. Generate JWT Token
            account.Token = tokenService.CreateToken(data, config);

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
                data.Account.Token = tokenService.CreateToken(account, config);
                
                // Get Menu
                data.Menu = GetListMenu(new GetByIdRequest {Id = account.Id}).Result.Data
                    .Select(x => new MenuModel()
                    {
                        Action = x.Action,
                        Controller = x.Controller,
                        GroupPermissionId = x.GroupPermissionId,
                        Name = x.Name,
                        Sort = x.Sort,
                        IsShowMenu = x.IsShowMenu
                    }).ToList();
                
                // Get Permission
                data.Permission = GetPermission(new GetByIdRequest { Id = account.Id }).Result.Data;
                
                // Get Group Permission
                data.GroupPermission = groupPermissionRepository.GetList(new GetAllRequest()).Result.Data.Where(x => x.IsActived == true)
                    .Select(x => new GroupPermissionModel()
                    {
                        Id = x.Id,
                        Sort = x.Sort,
                        Name = x.Name,
                        Icon = x.Icon,
                    }).ToList();
                
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

    public async Task<BaseResponse<GetListPagingResponse>> GetListPaging(GetListPagingRequest request)
    {
        var response = new BaseResponse<GetListPagingResponse>();

        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iTextSearch", request.Search, DbType.String);
            parameters.Add("@iPageIndex", request.Offset, DbType.Int32);
            parameters.Add("@iRowsPerPage", request.Limit, DbType.Int32);
            parameters.Add("@oTotalRow", dbType: DbType.Int64, direction: ParameterDirection.Output);

            var result = await unitOfWork.GetRepository<UserModel>().ExecWithStoreProcedure("sp_Sys_User_GetListPaging", parameters);


            var totalRow = parameters.Get<long>("@oTotalRow");
            var responseData = new GetListPagingResponse
            {
                PageIndex = request.Offset,
                Data = result,
                TotalRow = Convert.ToInt32(totalRow)
            };

            response.Data = responseData;
        }
        catch (Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<UserModel>> GetById(GetByIdRequest request)
    {
        var response = new BaseResponse<UserModel>();

        try
        {
            var data = await unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new Exception("Not data found");
            }

            var result = mapper.Map<UserModel>(data);
            response.Data = result;
        }
        catch (Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<UserDto>> GetByPost(GetByIdRequest request)
    {
        var response = new BaseResponse<UserDto>();

        try
        {
            var result = new UserDto();
            var data = await unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);
            if (result == null)
            {
                result.Id = Guid.NewGuid();
                result.IsEdit = false;
            }
            else
            {
                result = mapper.Map<UserDto>(data);
                result.IsEdit = true;
            }

            response.Data = result;

        }
        catch (Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<UserModel>> Insert(UserDto request)
    {
        var response = new BaseResponse<UserModel>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<User>().Find(x =>
                !x.IsDeleted &&
                x.StaffCode == request.StaffCode);
            if (checkData != null)
            {
                throw new Exception("Data already exists");
            }

            var entity = mapper.Map<User>(request);
            entity.CreatedBy = contextAccessor.HttpContext.User.Identity.Name;
            entity.CreatedAt = DateTime.Now; ;
            entity.UpdatedBy = contextAccessor.HttpContext.User.Identity.Name;
            entity.UpdatedAt = DateTime.Now;

            var result = await unitOfWork.GetRepository<User>().AddAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();

            response.Data = mapper.Map<UserModel>(result);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }

    public async Task<BaseResponse<UserModel>> Update(UserDto request)
    {
        var response = new BaseResponse<UserModel>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<User>().Find(x =>
                !x.IsDeleted &&
                x.Id != request.Id &&
                x.StaffCode == request.StaffCode);
            if (checkData != null)
            {
                throw new Exception("Data already exists");
            }

            var data = await unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new Exception("Not data found");
            }
            var entity = mapper.Map(request, data);

            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = contextAccessor.HttpContext.User.Identity.Name;

            await unitOfWork.GetRepository<User>().UpdateAsync(entity);

            await unitOfWork.SaveChangesAsync();
            await unitOfWork.CommitAsync();

            response.Data = mapper.Map<UserModel>(entity);
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }


    public async Task<BaseResponse<string>> DeLeteList(DeleteListRequest request)
    {
        var response = new BaseResponse<string>();
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();
            foreach (var id in request.Ids)
            {
                var entity = await unitOfWork.GetRepository<User>().GetByIdAsync(id);

                entity.IsDeleted = true;
                entity.DeletedAt = DateTime.Now;
                entity.DeletedBy = contextAccessor.HttpContext.User.Identity.Name;

                await unitOfWork.GetRepository<User>().UpdateAsync(entity);

                await unitOfWork.SaveChangesAsync();
            }
            await unitOfWork.CommitAsync();

            response.Data = "Success";
        }
        catch (Exception ex)
        {
            await unitOfWork.RollbackAsync();
            response.Error = true;
            response.Message = ex.Message;
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

            response.Data = await unitOfWork.GetRepository<PermissionModel>().ExecWithStoreProcedure("sp_Sys_Account_GetPermissionWithAccount", parameters);
        }
        catch (Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }
    
    //GET MENU
    public async Task<BaseResponse<List<MenuModel>>> GetListMenu(GetByIdRequest request)
    {
        var response = new BaseResponse<List<MenuModel>>();
        try
        {
            var parameters = new DynamicParameters();
            parameters.Add("@iAccountId", request.Id);

            response.Data = await unitOfWork.GetRepository<MenuModel>().ExecWithStoreProcedure("sp_Sys_Account_GetMenu", parameters);
        }
        catch (Exception ex)
        {
            response.Error = true;
            response.Message = ex.Message;
        }

        return response;
    }
}