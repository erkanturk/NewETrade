namespace ETICARET.Business.Settings;

/// <summary>
/// appsettings.json → "Email" bölümü (Options pattern ile bu sınıfa dolar).
/// Password: Gmail hesap şifresi değil, Google'ın verdiği 16 haneli "uygulama şifresi".
/// </summary>
public class EmailSettings
{
    public string Host { get; set; } = string.Empty;
    public int Port { get; set; } = 587;
    public bool EnableSsl { get; set; } = true;
    public string UserName { get; set; } = string.Empty;
    public string Password { get; set; } = string.Empty;
    public string FromName { get; set; } = "ETİCARET";
}
