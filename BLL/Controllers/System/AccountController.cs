using System.Net;
using BLL.Helpers;
using DTO.Base;
using DTO.Category.Shift.Dtos;
using DTO.System.Account.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REPOSITORY.System.Account;

namespace BLL.Controllers.System;

public class AccountController(IAccountRepository repository) : BaseController<AccountController>
{
    [HttpPost, Route("register")]
    public async Task<IActionResult> Register(RegisterDto request)
    {
        try
        {
            var result = await repository.Register(request);
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpPost, Route("login")]
    [AllowAnonymous]
    public async Task<IActionResult> Login(AccountDto request)
    {
        try
        {
            var result = await repository.Login(request);
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpPost, Route("get-list")]
    public async Task<IActionResult> GetListAsync(GetListPagingRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
            }
            var result = await repository.GetListPaging(request);
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpPost("get-by-id")]
    public async Task<IActionResult> GetById(GetByIdRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
            }
            var result = await repository.GetById(request);
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpPost("get-by-post")]
    public async Task<IActionResult> GetByPost(GetByIdRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
            }
            var result = await repository.GetByPost(request);
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpPost("insert")]
    public async Task<IActionResult> Post(UserDto request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
            }
            var result = await repository.Insert(request);
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpPost("update")]
    public async Task<IActionResult> Update(UserDto request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
            }
            var result = await repository.Update(request);
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }

    [HttpPost("delete-list")]
    public async Task<IActionResult> Detele(DeleteListRequest request)
    {
        try
        {
            if (!ModelState.IsValid)
            {
                throw new Exception(DTO.Common.CommonFunc.GetModelStateAPI(ModelState));
            }
            var result = await repository.DeLeteList(request);
            if (result.Error)
            {
                throw new Exception(result.Message);
            }
            else
            {
                return Ok(new ApiOkResponse(result.Data));
            }
        }
        catch (Exception ex)
        {
            return Ok(new ApiResponse(false, (int)HttpStatusCode.InternalServerError, ex.Message));
        }
    }
}