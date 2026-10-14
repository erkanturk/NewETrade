namespace ETICARET.Business.Models;

/// <summary>
/// İşlemin sonucu: başarılı mı + kullanıcıya gösterilecek mesaj.
/// Business katmanı hata durumunda exception fırlatmak yerine bunu döner;
/// Controller mesajı ekranda gösterir.
///
/// Kullanım:  return ServiceResult.Ok("Ürün eklendi.");
///            return ServiceResult.Fail("Stok yetersiz.");
/// </summary>
public record ServiceResult(bool Success, string Message)
{
    public static ServiceResult Ok(string message = "İşlem başarılı.") => new(true, message);

    public static ServiceResult Fail(string message) => new(false, message);
}
