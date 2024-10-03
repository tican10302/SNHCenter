using Microsoft.AspNetCore.Mvc;

namespace GUI_backup.Controllers.System
{
    public class ErrorController : BaseController<ErrorController>
    {
        public IActionResult Index()
        {
            return View("~/Views/System/Error/Index.cshtml");
        }
    }
}
