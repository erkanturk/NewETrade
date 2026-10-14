using System.ComponentModel.DataAnnotations;

namespace ETICARET.WebUI.Models.Account;

// Hesap formları basit olduğu için Data Annotations ile doğrulanır.
// Bu attribute'lar hem sunucuda hem tarayıcıda (jquery-validation-unobtrusive) çalışır.

public class LoginModel
{
    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; } = string.Empty;

    [Display(Name = "Beni hatırla")]
    public bool RememberMe { get; set; }

    public string? ReturnUrl { get; set; }
}

public class RegisterModel
{
    [Required(ErrorMessage = "Ad soyad zorunludur.")]
    [StringLength(100, ErrorMessage = "En fazla 100 karakter.")]
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "3-50 karakter arasında olmalıdır.")]
    [Display(Name = "Kullanıcı adı")]
    public string UserName { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre zorunludur.")]
    [DataType(DataType.Password)]
    [Display(Name = "Şifre")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Şifreler eşleşmiyor.")]
    [Display(Name = "Şifre (tekrar)")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class ForgotPasswordModel
{
    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;
}

public class ResetPasswordModel
{
    // E-postadaki linkten gelen token (gizli alan olarak taşınır)
    [Required]
    public string Token { get; set; } = string.Empty;

    [Required(ErrorMessage = "E-posta zorunludur.")]
    [EmailAddress(ErrorMessage = "Geçerli bir e-posta girin.")]
    [Display(Name = "E-posta")]
    public string Email { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yeni şifre zorunludur.")]
    [DataType(DataType.Password)]
    [Display(Name = "Yeni şifre")]
    public string Password { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
    [DataType(DataType.Password)]
    [Compare(nameof(Password), ErrorMessage = "Şifreler eşleşmiyor.")]
    [Display(Name = "Yeni şifre (tekrar)")]
    public string ConfirmPassword { get; set; } = string.Empty;
}

public class ProfileModel
{
    [Required(ErrorMessage = "Ad soyad zorunludur.")]
    [StringLength(100)]
    [Display(Name = "Ad Soyad")]
    public string FullName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Kullanıcı adı zorunludur.")]
    [StringLength(50, MinimumLength = 3, ErrorMessage = "3-50 karakter arasında olmalıdır.")]
    [Display(Name = "Kullanıcı adı")]
    public string UserName { get; set; } = string.Empty;

    [Phone(ErrorMessage = "Geçerli bir telefon numarası girin.")]
    [Display(Name = "Telefon")]
    public string? PhoneNumber { get; set; }

    // Sadece gösterilir, formdan güncellenmez.
    [Display(Name = "E-posta")]
    public string? Email { get; set; }
}

public class ChangePasswordModel
{
    [Required(ErrorMessage = "Mevcut şifre zorunludur.")]
    [DataType(DataType.Password)]
    [Display(Name = "Mevcut şifre")]
    public string CurrentPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Yeni şifre zorunludur.")]
    [DataType(DataType.Password)]
    [Display(Name = "Yeni şifre")]
    public string NewPassword { get; set; } = string.Empty;

    [Required(ErrorMessage = "Şifre tekrarı zorunludur.")]
    [DataType(DataType.Password)]
    [Compare(nameof(NewPassword), ErrorMessage = "Şifreler eşleşmiyor.")]
    [Display(Name = "Yeni şifre (tekrar)")]
    public string ConfirmPassword { get; set; } = string.Empty;
}
