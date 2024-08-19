using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REPOSITORY.System.Account;

namespace BLL.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BaseController<T> : ControllerBase
{
    private IAccountRepository _account { get; set; }
    public BaseController(IAccountRepository account)
    {
        _account = account;
    }

    public BaseController()
    {
        
    }
    
    public string GetUserName()
    {
        return HttpContext.User.Claims.Where(x => x.Type == "name").FirstOrDefault().Value.ToString();
    }
}