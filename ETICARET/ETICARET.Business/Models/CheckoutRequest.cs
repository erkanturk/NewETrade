using ETICARET.Entities;

namespace ETICARET.Business.Models;

/// <summary>
/// Sipariş verme isteği. Controller formdan gelen bilgiyi buna çevirip OrderManager'a verir.
/// NOT: "init" → değer sadece nesne oluşturulurken atanabilir, sonra değiştirilemez.
/// </summary>
public record CheckoutRequest
{
    public required string UserId { get; init; }
    public required string FirstName { get; init; }
    public required string LastName { get; init; }
    public required string Email { get; init; }
    public required string Phone { get; init; }
    public required string City { get; init; }
    public required string Address { get; init; }
    public string? Note { get; init; }
    public PaymentType PaymentType { get; init; }

    // Sadece kredi kartı ile ödemede dolu gelir.
    public CardInfo? Card { get; init; }

    public string IpAddress { get; init; } = "127.0.0.1";
}

public record CardInfo(string HolderName, string Number, string ExpireMonth, string ExpireYear, string Cvc);
