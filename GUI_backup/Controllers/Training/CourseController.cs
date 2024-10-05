using Microsoft.AspNetCore.Mvc;

namespace GUI_backup.Controllers.Training
{
    public class CourseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
