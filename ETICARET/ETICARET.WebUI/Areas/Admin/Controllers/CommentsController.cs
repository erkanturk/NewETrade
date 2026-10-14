using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Areas.Admin.Controllers
{
    public class CommentsController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
