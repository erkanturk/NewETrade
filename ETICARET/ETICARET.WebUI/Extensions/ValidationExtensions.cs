using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace ETICARET.WebUI.Extensions
{
    public static class ValidationExtensions
    {
        extension(ValidationResult result)
        {
            public void AddToModelState(ModelStateDictionary modelState)
            {
                foreach (var error in result.Errors)
                {
                    modelState.AddModelError(error.PropertyName, error.ErrorMessage);
                }
            }
        }
    }
}
