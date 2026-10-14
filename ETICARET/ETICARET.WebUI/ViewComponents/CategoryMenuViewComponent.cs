using ETICARET.Business.Abstract;
using ETICARET.WebUI.Models;
using Microsoft.AspNetCore.Mvc;

namespace ETICARET.WebUI.ViewComponents;

/// <summary>
/// View Component: Kendi verisini kendisi çeken, tekrar kullanılabilir sayfa parçası.
/// Partial View'dan farkı: Controller'a ihtiyaç duymadan servis çağırabilir.
///
/// Kullanım:
///   @await Component.InvokeAsync("CategoryMenu")                        → Default.cshtml (üst menü)
///   @await Component.InvokeAsync("CategoryMenu", new { view = "Sidebar" }) → Sidebar.cshtml (yan menü)
/// View'lar: Views/Shared/Components/CategoryMenu/
/// </summary>
public class CategoryMenuViewComponent(ICategoryService categoryService) : ViewComponent
{
    public async Task<IViewComponentResult> InvokeAsync(string view = "Default")
    {
        var model = new CategoryMenuViewModel(
            await categoryService.GetAllWithCountAsync(),
            RouteData.Values["category"]?.ToString());

        return View(view, model);
    }
}
