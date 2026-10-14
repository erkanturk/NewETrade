using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Areas.Admin.Controllers
{
    public class DashboardController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
