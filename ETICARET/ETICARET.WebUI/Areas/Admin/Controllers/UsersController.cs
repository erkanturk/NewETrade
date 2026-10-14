using ETICARET.Entities.Identity;
using ETICARET.WebUI.Areas.Admin.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace ETICARET.WebUI.Areas.Admin.Controllers
{
    public class UsersController(UserManager<ApplicationUser> userManager) : AdminBaseController
    {
        public async Task<IActionResult> Index()
        {
            var adminIds = (await userManager.GetUsersInRoleAsync(Roles.Admin)).Select(u => u.Id).ToHashSet();

            var users = await userManager.Users.OrderBy(u => u.UserName)
                .Select(u => new UserListItem(u.Id, u.UserName?? "", u.FullName, u.Email?? "", u.EmailConfirmed, false)).ToListAsync();
            return View(users.Select(u => u with { IsAdmin=adminIds.Contains(u.Id) }).ToList());

        }
        [HttpPost]
        public async Task<IActionResult> ToggleAdmin(string id)
        {
            if (id==UserId)
            {
                Notify("Kendi admin yetkini kaldıramazsın", "warning");
                return RedirectToAction(nameof(Index));

            }
            var user = await userManager.FindByIdAsync(id);
            if (user is null)
                return NotFound();
            if (await userManager.IsInRoleAsync(user, Roles.Admin))
            {
                await userManager.RemoveFromRoleAsync(user, Roles.Admin);
                Notify($"{user.UserName} artık admin değil", "info");
            }
            else
            {
                await userManager.AddToRoleAsync(user, Roles.Admin);
                Notify($"{user.UserName} admin yapıldı");
            }
            return RedirectToAction(nameof(Index));
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            if (id==UserId)
            {
                Notify($"Kendi hesabını silemezsin", "warning");
                return RedirectToAction(nameof(Index));
            }
            var user =await userManager.FindByIdAsync(id);
            if (user is null)
            {
                return NotFound();
            }
            await userManager.DeleteAsync(user);
            Notify($"{user.UserName} silindi", "info");
            return RedirectToAction(nameof(Index));
        }
    }
}
