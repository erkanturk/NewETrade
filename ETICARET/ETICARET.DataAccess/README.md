# ETICARET.DataAccess — Veri Erişim Katmanı

Veritabanıyla konuşan **tek** katman. EF Core sorguları sadece burada yazılır.
Bağımlılık: `ETICARET.Entities`

| Klasör / Dosya | İçerik |
|---|---|
| `Abstract/` | Arayüzler: `IRepository<T>` (ortak CRUD) ve tabloya özel `IProductDal`, `ICartDal`... |
| `Concrete/EfCore/EticaretContext.cs` | DbContext: tablolar (`DbSet`) + Fluent API ayarları. Identity tabloları da burada. |
| `Concrete/EfCore/EfCoreGenericRepository.cs` | `IRepository<T>`'nin EF Core hali; diğer Dal sınıfları bundan miras alır |
| `Concrete/EfCore/EfCore*Dal.cs` | Tabloya özel sorgular (sayfalama, arama, Include...) |
| `Seed/CatalogSeed.cs` | Örnek 6 kategori + 88 ürün (veritabanı boşsa eklenir) |
| `Migrations/` | `dotnet ef migrations add` ile üretilen dosyalar (elle düzenlenmez) |
| `DataAccessServiceRegistration.cs` | `AddDataAccess()` → DbContext ve Dal'ları DI'a kaydeder |

## Öğrenciye notlar
- **Repository deseni:** Üst katman (Business) "nasıl" sorgulandığını bilmez, sadece `IProductDal` arayüzünü bilir. Yarın EF yerine Dapper kullanılsa Business değişmez.
- **DbContext DI ile gelir:** `new DataContext()` yok. Bir HTTP isteği boyunca tek DbContext kullanılır (Scoped).
- **`AsNoTracking()`**: Sadece okunacak (ekrana basılacak) verilerde kullanılır, daha hızlıdır.
- **`Include` / `ThenInclude`**: İlişkili tabloları birlikte getirir (JOIN). `Include(p => p.Images.Take(1))` → sadece ilk resim (Filtered Include).
- **`IQueryable`**: `Where`, `OrderBy`, `Skip`, `Take` eklenerek sorgu inşa edilir; `ToListAsync()` denince SQL çalışır.
- **`ExecuteDeleteAsync()`**: Kayıtları belleğe almadan doğrudan `DELETE` çalıştırır (sepeti boşaltma).
- **Çoka-çok:** `Product.Categories` ↔ `Category.Products`; ara tablo `ProductCategories` otomatik.

## Migration komutları
Komutlar `ETICARET` klasöründe (çözümün olduğu yerde) çalıştırılır:
```bash
dotnet tool restore
dotnet ef migrations add YeniAlan --project ETICARET.DataAccess --startup-project ETICARET.WebUI
dotnet ef database update --project ETICARET.DataAccess --startup-project ETICARET.WebUI
```
Visual Studio Package Manager Console: Default project = `ETICARET.DataAccess`, Startup = `ETICARET.WebUI`, sonra `Add-Migration YeniAlan` ve `Update-Database`.

> Uygulama açılışta `MigrateAsync()` çağırdığı için `database update` yazmadan da tablolar oluşur.

## Eski → Yeni
| Eski | Yeni |
|---|---|
| İki ayrı context (`DataContext` + `ApplicationIdentityDbContext`), iki ayrı migration geçmişi, WebUI'de de kopya migration | Tek `EticaretContext : IdentityDbContext`, tek migration |
| Bağlantı cümlesi `OnConfiguring` içinde sabit yazılıydı | `appsettings.json` → `AddDbContext` |
| Her metotta `using var context = new DataContext()` | Context constructor'dan gelir (primary constructor) |
| Her metodun senkron + asenkron iki kopyası | Sadece `async` metotlar |
| `ExecuteSqlRaw("delete from CartItem ...")` | `ExecuteDeleteAsync()` |
| Arama: bütün ürünler belleğe çekilip C#'ta filtreleniyordu | `Where(...Contains...)` → SQL `LIKE`, sayfalama veritabanında |
| `ProductCategory` ara sınıfı + `Include(...).ThenInclude(pc => pc.Category)` | `Include(p => p.Categories)` |
| `SeedDatabase`: hatalı dosya adları, tekrar eden ürünler | `CatalogSeed`: 88 ürün, temiz dosya adları (`img/products/urun-adi-1.webp`) |
