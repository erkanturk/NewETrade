using Microsoft.AspNetCore.Diagnostics;

namespace ETICARET.WebUI.Infrastructure;

/// <summary>
/// .NET 8+ IExceptionHandler: Yakalanmayan bütün hatalar buraya düşer (UseExceptionHandler ile).
/// - Hatayı loglar.
/// - /api isteklerine JSON (ProblemDetails) döner.
/// - Normal sayfalarda false döner → kullanıcı /Home/Error hata sayfasını görür.
///
/// Eski: Her şeyi JSON'a çeviren elle yazılmış middleware (MVC sayfalarını da JSON yapıyordu).
/// </summary>
public class GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger, IProblemDetailsService problemDetailsService)
    : IExceptionHandler
{
    public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
    {
        logger.LogError(exception, "Beklenmeyen hata: {Method} {Path}", httpContext.Request.Method, httpContext.Request.Path);

        if (!httpContext.Request.Path.StartsWithSegments("/api"))
            return false;

        httpContext.Response.StatusCode = StatusCodes.Status500InternalServerError;
        return await problemDetailsService.TryWriteAsync(new ProblemDetailsContext
        {
            HttpContext = httpContext,
            Exception = exception,
            ProblemDetails =
            {
                Title = "Sunucu hatası",
                Detail = "Beklenmeyen bir hata oluştu. Lütfen daha sonra tekrar deneyin.",
                Status = StatusCodes.Status500InternalServerError
            }
        });
    }
}
