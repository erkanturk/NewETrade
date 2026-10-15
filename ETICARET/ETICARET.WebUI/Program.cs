using ETICARET.Business;
using ETICARET.Business.Settings;
using ETICARET.DataAccess;
using ETICARET.DataAccess.Concrete.EfCore;
using ETICARET.Entities.Identity;
using ETICARET.WebUI.Controllers.Api;
using ETICARET.WebUI.Data;
using ETICARET.WebUI.Infrastructure;
using FluentValidation;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Scalar.AspNetCore;
using System.Globalization;

CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDataAccess(builder.Configuration.GetConnectionString("DefaultConnection")!);
builder.Services.AddBusiness();
builder.Services.Configure<IyzicoSettings>(builder.Configuration.GetSection("Iyzico"));
builder.Services.Configure<EmailSettings>(builder.Configuration.GetSection("Email"));
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    options.Password.RequiredLength=8;
    options.Password.RequireDigit=true;
    options.Password.RequireUppercase=true;
    options.Password.RequireLowercase=true;
    options.Password.RequireNonAlphanumeric=true;

    options.Lockout.MaxFailedAccessAttempts=5;
    options.Lockout.DefaultLockoutTimeSpan=TimeSpan.FromMinutes(5);

    options.User.RequireUniqueEmail=true;
    options.User.AllowedUserNameCharacters+="çğıöşüÇĞİÖŞÜ";
    options.SignIn.RequireConfirmedEmail=true;
}).AddEntityFrameworkStores<EticaretContext>().AddDefaultTokenProviders().AddErrorDescriber<TurkishIdentityErrorDescriber>();

builder.Services.ConfigureApplicationCookie(options =>
{
    options.LoginPath="/Account/Login";
    options.LogoutPath="/Account/Logout";
    options.AccessDeniedPath="/Account/AccessDenied";
    options.SlidingExpiration=true;
    options.Cookie.Name=".Eticaret.Auth";
    options.Cookie.HttpOnly=true;
});

// Add services to the container.
builder.Services.AddControllersWithViews(options =>
{
    //Her actiona tanımlamak yerine bu alanda belirttik.
    options.Filters.Add(new AutoValidateAntiforgeryTokenAttribute());
    //Zorunlu alanarı biz açıkça belirtiyoruz.
    options.SuppressImplicitRequiredAttributeForNonNullableReferenceTypes=true;

    var messages = options.ModelBindingMessageProvider;
    messages.SetValueMustBeANumberAccessor(field => $"{field} alanına sayı girilmelidir.");
    messages.SetAttemptedValueIsInvalidAccessor((value, field) => $"\"{value}\" degeri {field} alanı için geçerlidir.");
    messages.SetValueMustNotBeNullAccessor(field => $"{field} alanı boş bırakılamaz.");
    messages.SetMissingBindRequiredValueAccessor(field => $"{field} alanı zorunludur.");
});
builder.Services.AddValidatorsFromAssemblyContaining<Program>();

builder.Services.AddOpenApi();
builder.Services.AddCors(options => options.AddPolicy(ApiBaseController.CorsPolicyName, policy => policy
.WithOrigins(builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>()?? []).AllowAnyHeader().AllowAnyMethod()));

builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
builder.Services.AddProblemDetails();

var app = builder.Build();

await app.InitializeDatabaseAsync();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
    app.UseHttpsRedirection();
}

app.UseStatusCodePagesWithReExecute("/Home/Status/{0}");

app.UseRouting();
app.UseStaticFiles();
app.UseCors();
app.UseAuthorization();
app.UseAuthentication();

app.MapStaticAssets();
app.MapControllerRoute(
    name: "admin",
    pattern: "{area:exists}/{controller=Dashboard}/{action=Index}/{id?}"
    );

app.MapControllerRoute(
    name: "products",
    pattern: "products/{category?}",
    defaults: new { controller = "Shop", action = "List" }
    );

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
    ).WithStaticAssets();

app.Run();
