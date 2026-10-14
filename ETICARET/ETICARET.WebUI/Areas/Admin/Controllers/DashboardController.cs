using ETICARET.Business.Abstract;
using ETICARET.Entities.Identity;
using ETICARET.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETICARET.WebUI.Areas.Admin.Controllers
{
    public class DashboardController(IProductService productService,
        ICategoryService categoryService,
        IOrderService orderService,
        UserManager<ApplicationUser> userManager) : AdminBaseController
    {
        public async Task<IActionResult> Index()
        {
            var model = new DashboardViewModel(
                ProductCount: await productService.CountAsync(),
                CategoryCount: await categoryService.CountAsync(),
                OrderCount: await orderService.CountAsync(),
                UserCount: await userManager.Users.CountAsync(),
                Revenue:await orderService.GetTotalRevenueAsync(),
                LatestOrders:await orderService.GetLatestOrdersAsync(),
                LowStockProducts:await productService.GetLowStockAsync()
                );
            return View(model);
        }
    }
}
