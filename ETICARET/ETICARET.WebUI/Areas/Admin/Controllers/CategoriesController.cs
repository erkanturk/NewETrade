using ETICARET.Business.Abstract;
using ETICARET.Entities;
using ETICARET.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Mvc;
namespace ETICARET.WebUI.Areas.Admin.Controllers
{
    public class CategoriesController(ICategoryService categoryService) : AdminBaseController
    {
        public async Task<IActionResult> Index()
     => View(await categoryService.GetAllWithCountAsync());

        public IActionResult Create() => View(new CategoryFormModel());

        [HttpPost]
        public async Task<IActionResult> Create(CategoryFormModel model)
        {
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            var result = await categoryService.CreateAsync(new Category { Name=model.Name });
            if (!result.Success)
            {
                ModelState.AddModelError(nameof(model.Name), result.Message);
                return View(model);
            }
            Notify(result);
            return RedirectToAction(nameof(Index));
        }
        public async Task<IActionResult> Edit(int id)
        {
            var category = await categoryService.GetWithProductsAsync(id);
            if(category is null)
                return NotFound();
            return View(new CategoryFormModel { Id=category.Id, Name=category.Name, Products=category.Products });

        }
        [HttpPost]
        public async Task<IActionResult> Edit(CategoryFormModel model)
        {
            var category = await categoryService.GetWithProductsAsync(model.Id);
            if (category is null)
                return NotFound();

            model.Products=category.Products;
            if (!ModelState.IsValid)
                return View(model);
            category.Name=model.Name;
            var result = await categoryService.UpdateAsync(category);
            if (!result.Success)
            {
                ModelState.AddModelError(nameof(model.Name), result.Message);
                return View(model);
            }
            Notify(result);
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Notify(await categoryService.DeleteAsync(id));
            return RedirectToAction(nameof(Index));
        }
    }
}
