namespace ETICARET.WebUI.Models;

/// <summary>
/// Sayfanın sağ üstünde çıkan bildirim (toast).
/// Controller: Notify("Ürün eklendi.")  →  TempData'ya yazılır  →  Layout okuyup gösterir.
/// Type: success | danger | warning | info  (Bootstrap renkleri)
/// </summary>
public record NotifyMessage(string Text, string Type = "success")
{
    public const string Key = "Notify";
}
