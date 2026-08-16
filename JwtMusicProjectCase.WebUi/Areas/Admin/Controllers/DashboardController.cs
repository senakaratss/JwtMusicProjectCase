using Microsoft.AspNetCore.Mvc;

namespace JwtMusicProjectCase.WebUi.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
