using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using GUI.Models;

namespace GUI.Controllers;

public class HomeController : BaseController<HomeController>
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        return View();
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        // return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        return View("~/Views/System/Error/Index.cshtml");
    }
}