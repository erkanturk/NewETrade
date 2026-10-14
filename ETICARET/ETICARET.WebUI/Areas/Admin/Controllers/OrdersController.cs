using ETICARET.Business.Abstract;
using ETICARET.Entities;
using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Areas.Admin.Controllers
{
    public class OrdersController(IOrderService orderService) : AdminBaseController
    {
        public async Task<IActionResult> Index()
        {
            return View(await orderService.GetAllOrdersAsync());
        }
        public async Task<IActionResult> Details(int id)
        {
            var order = await orderService.GetOrderDetailsAsync(id);
            return order is null ? NotFound() : View(order);
        }
        [HttpPost]
        public async Task<IActionResult> UpdateState(int id, OrderState state)
        {
            Notify(await orderService.UpdateStateAsync(id, state));
            return RedirectToAction(nameof(Details), new { id });
        }
    }
}
