using ETICARET.Business.Abstract;
using ETICARET.Business.Concrete;
using Microsoft.Extensions.DependencyInjection;

namespace ETICARET.Business;

public static class BusinessServiceRegistration
{
    /// <summary>Program.cs'te: builder.Services.AddBusiness();</summary>
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        // Scoped: Her HTTP isteğinde yeni nesne, istek boyunca aynı nesne.
        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<ICartService, CartManager>();
        services.AddScoped<IOrderService, OrderManager>();
        services.AddScoped<ICommentService, CommentManager>();

        // Dış servisler
        services.AddScoped<IPaymentService, IyzicoPaymentService>();
        services.AddScoped<IEmailService, SmtpEmailService>();

        return services;
    }
}
