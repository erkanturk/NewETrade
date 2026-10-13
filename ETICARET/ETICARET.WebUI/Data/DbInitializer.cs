using ETICARET.DataAccess.Concrete.EfCore;
using ETICARET.DataAccess.Seed;
using ETICARET.Entities.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ETICARET.WebUI.Data;

public static class DbInitializer
{
    public static async Task InitializeDatabaseAsync(this WebApplication app)
    {
        using var scope = app.Services.CreateScope();
        var services = scope.ServiceProvider;
        var context = services.GetRequiredService<EticaretContext>();
        await context.Database.MigrateAsync();
        await CatalogSeed.SeedAsync(context);
        var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
        if (!await roleManager.RoleExistsAsync(Roles.Admin))
            await roleManager.CreateAsync(new IdentityRole(Roles.Admin));
        var userManager = services.GetRequiredService<UserManager<ApplicationUser>>();
        var configration = app.Configuration;

        await CreateUserAsync(userManager, configration.GetSection("SeedUsers:Admin").Get<SeedUser>(), Roles.Admin);
        await CreateUserAsync(userManager, configration.GetSection("SeedUsers:Customer").Get<SeedUser>(), role:null);
        

    }
    private static async Task CreateUserAsync(UserManager<ApplicationUser> userManager, SeedUser? seed, string? role)
    {
        if (seed is null||await userManager.FindByEmailAsync(seed.Email) is not null)
        {
            return;
        }
        var user = new ApplicationUser
        {
            UserName =seed.UserName,
            Email=seed.Email,
            FullName = seed.FullName,
            EmailConfirmed=true
        };
        var result = await userManager.CreateAsync(user, seed.Password);
        if (result.Succeeded&&role is not null) 
            await userManager.AddToRoleAsync(user, role);
    }

    private record SeedUser(string UserName, string Email, string Password, string FullName);
}
