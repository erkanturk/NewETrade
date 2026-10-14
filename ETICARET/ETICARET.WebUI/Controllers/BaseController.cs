using ETICARET.Business.Models;
using ETICARET.WebUI.Extensions;
using ETICARET.WebUI.Models;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace ETICARET.WebUI.Controllers
{
    public abstract class BaseController : Controller
    {
        //Cookie'deki clamden giriş yapmış kullanıcının ıdsini al
        protected string UserId => User.FindFirstValue(ClaimTypes.NameIdentifier)??string.Empty;

        protected void Notify(string text, string type = "success") => TempData.Put(NotifyMessage.Key, new NotifyMessage(text, type));
        protected void Notify(ServiceResult result) 
            => Notify(result.Message, result.Success ? "success" : "danger");
        protected async Task<bool> ValidateAsync<T>(IValidator<T> validator, T model)
        {
            var result= await validator.ValidateAsync(model);
            result.AddToModelState(ModelState);
            return ModelState.IsValid;
        }
        protected IActionResult RedirectToLocal(string? returnUrl,string fallbackAction="Index",string fallbackController="Home")
            =>Url.IsLocalUrl(returnUrl)
            ?LocalRedirect(returnUrl): 
            RedirectToAction(fallbackAction, fallbackController);

        
    }
}
