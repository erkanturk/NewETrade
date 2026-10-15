using ETICARET.Business.Abstract;
using ETICARET.WebUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace ETICARET.WebUI.Controllers
{
    public class HomeController(IProductService productService ,ICategoryService categoryService) : BaseController
    {
        public async Task<IActionResult> Index()
        {
            var model = new HomeViewModel(
                Categories: await categoryService.GetAllWithCountAsync(),
                FeaturedProducts: await productService.GetHomeProductsAsync(10),
                LatestProducts: await productService.GetLatestAsync(5)
                );
            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error() => View();
        public IActionResult Status(int id) => id==404 ? View("NotFound") : View("Error");

     
    }
}
