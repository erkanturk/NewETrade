using ETICARET.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Areas.Admin.Controllers
{
    public class CommentsController(ICommentService commentService) : AdminBaseController
    {
        public async Task<IActionResult> Index()
        {
            return View(await commentService.GetAllAsync());
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Notify(await commentService.DeleteAsync(id, UserId, isAdmin: true));
            return RedirectToAction(nameof(Index));
        }
    }
}
