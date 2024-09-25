using DTO.Base;
using DTO.Common;
using DTO.System.Account.Dtos;
using DTO.System.Account.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REPOSITORY.Common;
using REPOSITORY.System.Account;

namespace BLL.Controllers.System;

public class AccountController(IAccountRepository repository) : BaseController<AccountController>
{
    [HttpPost]
    [Route("register")]
    [AllowAnonymous]
    public async Task<ActionResult<AccountModel>> Register(RegisterDto request)
    {
        try
        {
            var result = await repository.Register(request);
            return Ok(result);
        }
        catch (Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPost]
    [Route("login")]
    [AllowAnonymous]
    public async Task<ActionResult<AccountPermissionModel>> Login(AccountDto request)
    {
        try
        {
            var result = await repository.Login(request);
            return Ok(result);
        }
        catch (ApiException ex)
        {
            return HandleApiException(ex);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost]
    [Route("get-list")]
    public async Task<ActionResult> GetListAsync(GetListPagingRequest request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.GetListPaging(request);
            return Ok(result);
        }
        catch (ApiException ex)
        {
            return HandleApiException(ex);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.GetById(new GetByIdRequest { Id = id });
            return Ok(result);
        }
        catch (ApiException ex)
        {
            return HandleApiException(ex);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpGet("get-by-post/{id:guid}")]
    public async Task<IActionResult> GetByPost(Guid id)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            var result = await repository.GetByPost(new GetByIdRequest { Id = id });
            return Ok(result);
        }
        catch (ApiException ex)
        {
            return HandleApiException(ex);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost]
    public async Task<IActionResult> Create(UserDto request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            await repository.Insert(request);
            return Created();
        }
        catch (ApiException ex)
        {
            return HandleApiException(ex);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPut]
    public async Task<IActionResult> Update(UserDto request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            await repository.Update(request);
            return Ok();
        }
        catch (ApiException ex)
        {
            return HandleApiException(ex);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }

    [HttpPost("delete-list")]
    public async Task<IActionResult> DeleteList(DeleteListRequest request)
    {
        try
        {
            if (!ModelState.IsValid) throw new Exception(CommonFunc.GetModelStateAPI(ModelState));
            await repository.DeLeteList(request);
            return NoContent();
        }
        catch (ApiException ex)
        {
            return HandleApiException(ex);
        }
        catch (Exception ex)
        {
            return HandleException(ex);
        }
    }
}