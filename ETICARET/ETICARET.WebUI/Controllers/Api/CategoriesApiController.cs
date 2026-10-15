using ETICARET.Business.Abstract;
using ETICARET.WebUI.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Controllers.Api;

[Route("api/categories")]
public class CategoriesApiController(ICategoryService categoryService) : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult<List<CategoryDto>>> GetAll()
    {
        var categories = await categoryService.GetAllWithCountAsync();
        return categories.Select(c => new CategoryDto(c.Id, c.Name, c.ProductCount)).ToList();
    }
}
