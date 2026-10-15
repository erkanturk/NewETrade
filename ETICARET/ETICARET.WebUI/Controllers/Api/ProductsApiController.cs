using ETICARET.Business.Abstract;
using ETICARET.Entities.Dtos;
using ETICARET.WebUI.Extensions;
using ETICARET.WebUI.Models.Api;
using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.Controllers.Api;

[Route("api/products")]
public class ProductsApiController(IProductService productService) : ApiBaseController
{
    [HttpGet]
    public async Task<ActionResult<PagedResponse<ProductListItemDto>>> GetAll(string? category, string? search, int page = 1, int pageSize = 12)
    {
        pageSize=Math.Clamp(pageSize, 1, 50);
        page=Math.Max(1, page);
        var result = await productService.GetProductsAsync(
            new ProductFilter(category, search, ProductSort.Recommended, page, pageSize)
            );
        var items = result.Items
            .Select(p => new ProductListItemDto(p.Id, p.Name, p.Price, p.Stock, AbsoluteUrl(p.CoverImage))).ToList();

        return new PagedResponse<ProductListItemDto>(items, page, pageSize, result.TotalCount);
    }

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<ProductDetailDto>> GetById(int id)
    {
        var product = await productService.GetDetailsAsync(id);
        if (product is null)
            return NotFound();

        return new ProductDetailDto(
            product.Id,
            product.Name,
            product.Description,
            product.Price,
            product.Stock,
            [..product.Images.Select(i=>AbsoluteUrl(i.Src))],
            [.. product.Categories.Select(c=>c.Name)]
            );
    }

}
