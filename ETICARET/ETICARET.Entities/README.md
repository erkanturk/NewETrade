# ETICARET.Entities — Varlık (Entity) Katmanı

Veritabanı tablolarının C# karşılıkları burada. **Hiçbir katmana bağımlı değildir**; diğer bütün katmanlar bu projeyi kullanır.

| Klasör / Dosya | İçerik |
|---|---|
| `BaseEntity.cs` | Bütün tabloların ortak alanları (`Id`, `CreatedDate`) |
| `Product`, `Category`, `Image`, `Comment` | Katalog |
| `Cart`, `CartItem` | Sepet |
| `Order`, `OrderItem`, `OrderState`, `PaymentType` | Sipariş |
| `Identity/` | `ApplicationUser` (Identity kullanıcısı) ve `Roles` sabitleri |
| `Dtos/` | Tablo olmayan, sadece veri taşıyan `record` tipler (filtre, sayfalama sonucu) |

## Öğrenciye notlar
- **İlişkiler:** `Product` ↔ `Category` çoka-çok (`List<Category>` / `List<Product>`), `Product` → `Image` bire-çok (`ProductId` + `Product`).
- **`required`**: Nesne oluşturulurken verilmesi zorunlu alan (C# 11).
- **`string?`**: Boş olabilir (NULL). `string` ise boş olamaz → veritabanında `NOT NULL`.
- **`[MaxLength(200)]`**: Kolon uzunluğu `nvarchar(200)` olur. Yazılmazsa `nvarchar(max)`.
- **Hesaplanan özellik** (`TotalPrice => ...`): Setter'ı olmadığı için veritabanına yazılmaz.
- **`[]`**: Boş liste oluşturur (C# 12 collection expression).

## Eski → Yeni
| Eski | Yeni |
|---|---|
| Kullanılmayan `Address`, `Brand`, `Discount`, `Wishlist`, `ProductReview`, `RefreshToken` sınıfları | Kaldırıldı |
| `Product` içinde 30+ alan (SKU, barkod, ağırlık, SEO...) | Ekranda kullanılan alanlar: `Name`, `Description`, `Price`, `Stock`, `IsHome` |
| `ProductCategory` ara sınıfı elle yazılıyordu | `List<Category>` yeterli; ara tabloyu EF Core oluşturur |
| `BaseEntity` içinde soft delete, audit, `RowVersion` (hiç kullanılmıyordu) | Sadece `Id` ve `CreatedDate` |
| Enum'lar küçük harf (`waiting`, `unpaid`) | `OrderState.PaymentPending`, `Preparing`, `Shipped`... |
| `new List<T>()` | `[]` |
