using Microsoft.AspNetCore.Mvc;

namespace GUI.Controllers.Category;

public class ShiftController : Controller
{
    // GET
    public IActionResult Index()
    {
        return View("~/Views/Category/Shift/Index.cshtml");
    }
}