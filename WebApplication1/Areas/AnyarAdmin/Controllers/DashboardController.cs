using Microsoft.AspNetCore.Mvc;

namespace WebApplication1.Areas.AnyarAdmin.Controllers;
[Area("AnyarAdmin")]
public class DashboardController : Controller
{
    public IActionResult Index()
    {
        return View();
    }
}
