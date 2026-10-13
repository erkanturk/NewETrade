using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Controllers.Api
{
    public class ProductsApiController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
