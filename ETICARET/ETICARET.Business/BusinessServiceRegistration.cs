using ETICARET.Business.Abstract;
using ETICARET.Business.Concrete;
using Microsoft.Extensions.DependencyInjection;


namespace ETICARET.Business;

public static class BusinessServiceRegistration
{
    public static IServiceCollection AddBusiness(this IServiceCollection services)
    {
        services.AddScoped<ICommentService, CommentManager>();
        services.AddScoped<ICategoryService, CategoryManager>();
        services.AddScoped<IProductService, ProductManager>();
        services.AddScoped<ICartService, CartManager>();
        services.AddScoped<IOrderService, OrderManager>();
        services.AddScoped<IPaymentService, IyzicoPaymentService>();
        services.AddScoped<IEmailService, SmtpEmailService>();

        return services;
    }
}
