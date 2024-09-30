using System.Data;
using System.Net;
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
    public async Task<AccountModel> Register(RegisterDto request)
    {
        using var transaction = unitOfWork.BeginTransactionAsync();
        
        // 1. Check password
        if (request.Password != null && !request.Password.Equals(request.PasswordConfirm))
        {
            throw new ApiException((int)HttpStatusCode.BadRequest, "Password is not equal");
        }

        // 2. Check account exist
        var accountExist = await unitOfWork.GetRepository<DAL.Entities.Account>()
            .Find(x => x.UserName == request.UserName);

        if (accountExist != null)
        {
            throw new ApiException((int)HttpStatusCode.BadRequest, "Username is exist");
        }
        
        // 3. Hash password
        var entity = mapper.Map<DAL.Entities.Account>(request);
        entity.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
        
        // 4. Store to database
        entity.CreatedBy = contextAccessor.HttpContext?.User.Identity?.Name;
        entity.UpdatedBy = contextAccessor.HttpContext?.User.Identity?.Name;
        var data = await unitOfWork.GetRepository<DAL.Entities.Account>().AddAsync(entity);
        var account = mapper.Map<AccountModel>(data);
        
        await unitOfWork.SaveChangesAsync();
        await unitOfWork.CommitAsync();
        
        // 5. Generate JWT Token
        account.Token = tokenService.CreateToken(data, config);
        return account;
    }

    public async Task<AccountPermissionModel> Login(AccountDto request)
    {
        var data = new AccountPermissionModel();
        var account = await unitOfWork.GetRepository<DAL.Entities.Account>()
            .Find(x => x.UserName == request.UserName);
        if (account == null)
        {
            throw new ApiException((int)HttpStatusCode.BadRequest, "Username or password is not correct");
        }

        var checkPassword = BCrypt.Net.BCrypt.Verify(request.Password, account.PasswordHash);
        if (!checkPassword)
        {
            throw new ApiException((int)HttpStatusCode.BadRequest, "Username or password is not correct");
        }

        var userInfo = context.Users.Include(x => x.Role).FirstOrDefault(x => x.Account.Id == account.Id);

        if(userInfo == null)
        {
            throw new ApiException((int)HttpStatusCode.BadRequest, "User has not been activated yet");
        }
        var role = context.Roles.FirstOrDefault(x => x.Id == userInfo.Role.Id);

        if (role == null)
        {
            throw new ApiException((int)HttpStatusCode.Unauthorized, "User has not been authorized");
        }

        data.Account = mapper.Map<AccountModel>(account);
        data.Account.FirstName = userInfo.FirstName;
        data.Account.LastName = userInfo.LastName;
        data.Account.Role = role.Name;
        data.Account.Avatar = string.IsNullOrWhiteSpace(account.Avatar) ? "img/avatar/avatar-default.png" : account.Avatar;
        data.Account.Token = tokenService.CreateToken(account, config);
        data.UserName = data.Account.UserName;
        data.Token = data.Account.Token;
                    
        // Get Menu
        data.Menu = GetListMenu(new GetByIdRequest {Id = account.Id}).Result
            .Select(x => new MenuModel()
            {
                Action = x.Action,
                Controller = x.Controller,
                GroupPermissionId = x.GroupPermissionId,
                Icon = x.Icon,
                Name = x.Name,
                Sort = x.Sort,
                IsShowMenu = x.IsShowMenu
            }).ToList();
                    
        // Get Permission
        data.Permission = GetPermission(new GetByIdRequest { Id = account.Id }).Result;
                    
        // Get Group Permission
        data.GroupPermission = groupPermissionRepository.GetAll().Where(x => x.IsActived)
            .Select(x => new GroupPermissionModel()
            {
                Id = x.Id,
                Sort = x.Sort,
                Name = x.Name,
                Icon = x.Icon,
            }).ToList();
        return data;
    }

    public async Task<GetListPagingResponse> GetListPaging(GetListPagingRequest request)
    {
        var response = new GetListPagingResponse();
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
        return response;
    }

    public async Task<UserModel> GetById(GetByIdRequest request)
    {
        var data = await unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);
        if (data == null)
        {
            throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
        }
        var result = mapper.Map<UserModel>(data);
        return result;
    }

    public async Task<UserDto> GetByPost(GetByIdRequest request)
    {
        var result = new UserDto();
        var data = await unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);
        if (data == null)
        {
            result.Id = Guid.NewGuid();
            result.IsEdit = false;
        }
        else
        {
            result = mapper.Map<UserDto>(data);
            result.IsEdit = true;
        }
        return result;
    }

    public async Task<bool> Insert(UserDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<User>().Find(x =>
                !x.IsDeleted &&
                x.StaffCode == request.StaffCode);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }

            var entity = mapper.Map<User>(request);
            entity.CreatedBy = contextAccessor.HttpContext?.User.Identity?.Name;
            entity.CreatedAt = DateTime.Now;
            entity.UpdatedBy = contextAccessor.HttpContext?.User.Identity?.Name;
            entity.UpdatedAt = DateTime.Now;

            await unitOfWork.GetRepository<User>().AddAsync(entity);

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

    public async Task<bool> Update(UserDto request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();

            var checkData = await unitOfWork.GetRepository<User>().Find(x =>
                !x.IsDeleted &&
                x.Id != request.Id &&
                x.StaffCode == request.StaffCode);
            if (checkData != null)
            {
                throw new ApiException((int)HttpStatusCode.BadRequest, "Data already exists");
            }

            var data = await unitOfWork.GetRepository<User>().GetByIdAsync(request.Id);
            if (data == null)
            {
                throw new ApiException((int)HttpStatusCode.NotFound, "Not data found");
            }
            var entity = mapper.Map(request, data);

            entity.UpdatedAt = DateTime.Now;
            entity.UpdatedBy = contextAccessor.HttpContext?.User.Identity?.Name;

            await unitOfWork.GetRepository<User>().UpdateAsync(entity);

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


    public async Task<bool> DeLeteList(DeleteListRequest request)
    {
        try
        {
            using var transaction = unitOfWork.BeginTransactionAsync();
            foreach (var id in request.Ids)
            {
                var entity = await unitOfWork.GetRepository<User>().GetByIdAsync(id);

                if (entity != null)
                {
                    entity.IsDeleted = true;
                    entity.DeletedAt = DateTime.Now;
                    entity.DeletedBy = contextAccessor.HttpContext?.User.Identity?.Name;

                    await unitOfWork.GetRepository<User>().UpdateAsync(entity);
                }

                await unitOfWork.SaveChangesAsync();
            }
            await unitOfWork.CommitAsync();
        }
        catch (Exception)
        {
            await unitOfWork.RollbackAsync();
            throw;
        }
        
        return true;
    }

    //GET PERMISSION
    public async Task<List<PermissionModel>> GetPermission(GetByIdRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@iAccountId", request.Id);

        List<PermissionModel> response = await unitOfWork.GetRepository<PermissionModel>().ExecWithStoreProcedure("sp_Sys_Account_GetPermissionWithAccount", parameters);

        return response;
    }
    
    //GET MENU
    private async Task<List<MenuModel>> GetListMenu(GetByIdRequest request)
    {
        var parameters = new DynamicParameters();
        parameters.Add("@iAccountId", request.Id);

        List<MenuModel> response = await unitOfWork.GetRepository<MenuModel>().ExecWithStoreProcedure("sp_Sys_Account_GetMenu", parameters);
        return response;
    }
}