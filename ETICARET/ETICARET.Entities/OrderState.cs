namespace ETICARET.Entities;

/// <summary>Sipariş durumu. Veritabanında sayı (0, 1, 2...) olarak saklanır.</summary>
public enum OrderState
{
    PaymentPending = 0, // Havale/EFT bekleniyor
    Preparing = 1,      // Ödeme alındı, hazırlanıyor
    Shipped = 2,        // Kargoya verildi
    Completed = 3,      // Teslim edildi
    Cancelled = 4       // İptal edildi
}
