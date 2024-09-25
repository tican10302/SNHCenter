using BLL.Helpers;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BLL.Controllers.Base;

[Route("api/[controller]")]
[ApiController]
[Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
public class PingController : ControllerBase
{
    [HttpPost]
    public IActionResult Post()
    {
        return Ok(new ApiOkResponse("OK"));
    }
}