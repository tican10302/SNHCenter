using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using REPOSITORY.Common;

namespace BLL.Controllers;

[Route("api/[Controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class BaseController<T> : ControllerBase
{
    public string? GetUserName()
    {
        return HttpContext.User.Claims.Where(x => x.Type == "name").FirstOrDefault()?.Value.ToString();
    }

    protected ActionResult HandleApiException(ApiException ex)
    {
        return StatusCode(ex.StatusCode, new
        {
            status = ex.StatusCode,
            message = ex.Message,
            details = ex.Details
        });
    }

    protected ActionResult HandleException(Exception ex)
    {
        return StatusCode(500, new
        {
            status = 500,
            message = ex.Message,
        });
    }
}