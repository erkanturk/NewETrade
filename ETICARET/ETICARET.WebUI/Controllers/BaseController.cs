using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Controllers
{
    public class BaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
