namespace ETICARET.Business.Abstract;

public interface IEmailService
{
    /// <returns>Mail gönderildiyse true; ayar eksik veya hata varsa false.</returns>
    Task<bool> SendAsync(string to, string subject, string htmlBody);
}
