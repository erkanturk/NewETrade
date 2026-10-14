using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Controllers.Api
{
    public class ApiBaseController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
