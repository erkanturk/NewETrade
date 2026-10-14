using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Areas.Admin.Controllers
{
    public class AdminBaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
