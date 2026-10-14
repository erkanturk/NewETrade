namespace ETICARET.Business.Models;

public record PaymentResult(bool Success, string? PaymentId, string? ConversationId, string? ErrorMessage)
{
    public static PaymentResult Fail(string message) => new(false, null, null, message);
}
