# ETICARET.Business — İş Katmanı

Uygulamanın **kuralları** burada: "Stoktan fazla ürün sepete eklenemez", "Sipariş edilmiş ürün silinemez", "Yorumu sadece yazan düzenleyebilir" gibi.
Controller ince kalır; sadece formu alır, Business'ı çağırır, sonucu ekranda gösterir.
Bağımlılık: `ETICARET.DataAccess`, `ETICARET.Entities`

| Klasör / Dosya | İçerik |
|---|---|
| `Abstract/` | Servis arayüzleri: `IProductService`, `ICartService`, `IOrderService`, `IPaymentService`, `IEmailService`... |
| `Concrete/*Manager.cs` | İş kuralları (`ProductManager`, `CartManager`, `OrderManager`...) |
| `Concrete/IyzicoPaymentService.cs` | Kredi kartı ödemesi (Iyzico sandbox) |
| `Concrete/SmtpEmailService.cs` | E-posta gönderimi (ayar yoksa konsola yazar) |
| `Models/` | `ServiceResult` (işlem sonucu), `CheckoutRequest`, `PaymentResult` (record tipler) |
| `Settings/` | appsettings.json bölümlerinin sınıf karşılıkları (Options pattern) |
| `BusinessServiceRegistration.cs` | `AddBusiness()` → servisleri DI'a kaydeder |

## Öğrenciye notlar
- **`ServiceResult`**: Kural ihlalinde exception fırlatmak yerine `ServiceResult.Fail("mesaj")` döneriz; Controller mesajı bildirim olarak gösterir.
- **Sipariş akışı** (`OrderManager.PlaceOrderAsync`): sepet → stok kontrolü → (kartla ise) ödeme → sipariş + stok düşme → sepeti boşalt. Bütün adımlar tek metotta, okunabilir sırayla.
- **Aynı DbContext'in paylaşılması**: `cartDal` ile okunan ürünün stoğu değiştirilir, `orderDal.AddAsync` içindeki tek `SaveChanges` ikisini birlikte kaydeder. Çünkü DbContext **Scoped**: istek boyunca tek nesne.
- **Options pattern**: `IOptions<IyzicoSettings>` → appsettings.json'daki "Iyzico" bölümü otomatik olarak sınıfa dolar.
- **Dependency Inversion**: `OrderManager` Iyzico'yu tanımaz, sadece `IPaymentService` arayüzünü bilir. Başka ödeme firmasına geçmek için yeni bir sınıf yazıp DI kaydını değiştirmek yeter.
- **`record` + `with`**: `filter with { Page = 1 }` → kopyasını oluşturup tek alanı değiştirir.

## Eski → Yeni
| Eski | Yeni |
|---|---|
| Manager'lar sadece Dal'ı çağıran tek satırlık metotlardı, kurallar Controller'daydı | Kurallar Manager'da (stok, sipariş, yetki) |
| Iyzico kodu ve anahtarları `CartController` içinde sabit | `IPaymentService` + `appsettings.json` |
| Ödeme için dış IP `api.ipify.org`'dan her seferinde çekiliyordu | İstemci IP'si `HttpContext`'ten alınır |
| `MailHelper` static, Gmail şifresi kodun içinde | `IEmailService` + `EmailSettings` (appsettings.json → Options pattern) |
| JWT, refresh token, `BusinessException` hiyerarşisi, `ValidationExtensions`, DTO'lar (hiç kullanılmıyordu ya da 501 dönen API'ler içindi) | Kaldırıldı |
| Sipariş sonrası stok düşmüyordu | Stok sipariş ile birlikte düşer |
| Sepeti olmayan (admin panelden onaylanan) kullanıcı sepete ürün ekleyince hata | Sepet ilk üründe otomatik oluşur |
