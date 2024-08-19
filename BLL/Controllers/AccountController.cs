using System.Net;
using BLL.Helpers;
using DTO.System.Account.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REPOSITORY.System.Account;

namespace BLL.Controllers;

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
}