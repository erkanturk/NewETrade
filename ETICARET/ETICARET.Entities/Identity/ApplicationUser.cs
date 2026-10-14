using Microsoft.AspNetCore.Identity;

namespace ETICARET.Entities.Identity;

/// <summary>
/// Identity'nin hazır kullanıcı sınıfı (IdentityUser) Id, UserName, Email, PasswordHash gibi
/// alanları zaten içerir. Biz sadece ihtiyacımız olan ek alanı ekliyoruz.
/// </summary>
public class ApplicationUser : IdentityUser
{
    public string FullName { get; set; } = string.Empty;
}
