using ETICARET.Entities;
using System.Globalization;

namespace ETICARET.WebUI.Extensions
{
    public static class FormatExtensions
    {
        private static readonly CultureInfo Turkish = CultureInfo.GetCultureInfo("tr-TR");

        extension(decimal price)
        {
            //"₺1.234,50"
            public string ToTL() => price.ToString("C2", Turkish);
        }
        extension(DateTime date)
        {
            public string ToTrDate() => date.ToString("dd MMMM yyyy", Turkish);
            public string ToTrDateTime() => date.ToString("dd MMMM yyyy HH:mm", Turkish);

        }
        extension(OrderState state)
        {
            public string DisplayName => state switch
            {
                OrderState.PaymentPending => "Odeme bekleniyor",
                OrderState.Preparing => "Hazırlanıyor",
                OrderState.Shipped => "Kargoda",
                OrderState.Completed => "Teslim edildi",
                OrderState.Cancelled => "İptal edildi",
                _ => state.ToString()
            };

            public string BadgeClass => state switch
            {
                OrderState.PaymentPending => "status-warning",
                OrderState.Preparing => "status-info",
                OrderState.Shipped => "status-primary",
                OrderState.Completed => "status-success",
                _ => "status-muted"
            };
        }
        extension(PaymentType type)
        {
            public string DisplayName => type ==PaymentType.CreditCard ? "Kredi Kartı" : "Havale/Eft";
        }
    }
}
