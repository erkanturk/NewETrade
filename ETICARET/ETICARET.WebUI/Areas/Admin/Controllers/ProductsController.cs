using ETICARET.Business.Abstract;
using ETICARET.Entities;
using ETICARET.WebUI.Areas.Admin.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Areas.Admin.Controllers
{
    public class ProductsController(
        IProductService productService,
        ICategoryService categoryService,
        IValidator<ProductFormModel> validator,
        IWebHostEnvironment environment
        ) : AdminBaseController
    {
        public async Task<IActionResult> Index()
        {
            return View(await productService.GetAllForAdminAsync());

        }
        public async Task<IActionResult> Create()
        {
            return View(new ProductFormModel { AllCategories=await categoryService.GetAllAsync(), Stock=10 });
        }
        [HttpPost]
        public async Task<IActionResult> Create(ProductFormModel model)
        {
            if (!await ValidateAsync(validator, model))
                return await FormViewAsync(model);

            var product = new Product
            {
                Name = model.Name.Trim(),
                Description = model.Description?.Trim(),
                Price = model.Price,
                Stock = model.Stock,
                IsHome = model.IsHome
            };

            foreach (var file in model.Files)
                product.Images.Add(new Image { ImageUrl = await SaveImageAsync(file) });

            var result = await productService.CreateAsync(product, model.CategoryIds);
            if (!result.Success)
            {
                ModelState.AddModelError(string.Empty, result.Message);
                return await FormViewAsync(model);
            }

            Notify(result);
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var product = await productService.GetDetailsAsync(id);
            if (product is null)
                return NotFound();
            var model = new ProductFormModel
            {
                Id = id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                Stock= product.Stock,
                IsHome= product.IsHome,
                CategoryIds= [.. product.Categories.Select(c => c.Id)]
            };
            return await FormViewAsync(model);
        }
        [HttpPost]
        public async Task<IActionResult> Edit(ProductFormModel model)
        {
            var product = await productService.GetDetailsAsync(model.Id);
            if (product is null)
                return NotFound();
            if (!await ValidateAsync(validator, model))
                return await FormViewAsync(model);

            product.Name=model.Name.Trim();
            product.Description=model.Description?.Trim();
            product.Price=model.Price;
            product.Stock=model.Stock;
            product.IsHome=model.IsHome;

            product.Images.RemoveAll(i => model.DeleteImageIds.Contains(i.Id));
            foreach (var file in model.Files)
            {
                product.Images.Add(new Image { ImageUrl=await SaveImageAsync(file) });
            }
            var result = await productService.UpdateAsync(product, model.CategoryIds);
            Notify(result);
            return result.Success ? RedirectToAction(nameof(Index)) : await FormViewAsync(model);
        }
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            Notify(await productService.DeleteAsync(id));
            return RedirectToAction(nameof(Index));
        }

        private async Task<IActionResult> FormViewAsync(ProductFormModel model)
        {
            model.AllCategories= await categoryService.GetAllAsync();
            if (model.Id>0)
            {
                model.ExistingImages=(await productService.GetDetailsAsync(model.Id))?.Images?? [];

            }
            return View(model.Id==0 ? nameof(Create) : nameof(Edit), model);
        }
        private async Task<string> SaveImageAsync(IFormFile file)
        {
            var fileName = $"{Guid.NewGuid():N}{Path.GetExtension(file.FileName).ToLowerInvariant()}";
            var folder = Path.Combine(environment.WebRootPath, "img", "products");
            Directory.CreateDirectory(folder);

            await using var stream = System.IO.File.Create(Path.Combine(folder, fileName));
            return fileName;
        }
    }
}
