using System.Security.Claims;
using ETICARET.Business.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.ViewComponents;

/// <summary>Üst menüdeki sepet ikonu ve içindeki ürün sayısı.</summary>
public class CartBadgeViewComponent(ICartService cartService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync()
    {
        var userId = UserClaimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
        var count = userId is null ? 0 : await cartService.GetItemCountAsync(userId);
        return View(count);
    }
}
