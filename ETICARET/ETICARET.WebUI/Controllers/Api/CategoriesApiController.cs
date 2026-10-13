using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Controllers.Api
{
    public class CategoriesApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
