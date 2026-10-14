using ETICARET.Entities;
using ETICARET.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ETICARET.DataAccess.Concrete.EfCore;

/// <summary>
/// Veritabanının C# tarafındaki karşılığı.
/// IdentityDbContext'ten türediği için kullanıcı ve rol tabloları (AspNetUsers, AspNetRoles...)
/// da AYNI veritabanında, aynı migration ile oluşur.
///
/// NOT (C# 12 primary constructor): "(DbContextOptions&lt;EticaretContext&gt; options)" bu sınıfın yapıcı metodudur.
/// Bağlantı cümlesi burada YAZILMAZ; Program.cs'te AddDbContext ile appsettings.json'dan verilir.
/// </summary>
public class EticaretContext(DbContextOptions<EticaretContext> options)
    : IdentityDbContext<ApplicationUser>(options)
{
    // "=> Set<T>()" yazımı, nullable uyarısı vermeyen önerilen DbSet tanımıdır.
    public DbSet<Product> Products => Set<Product>();
    public DbSet<Category> Categories => Set<Category>();
    public DbSet<Image> Images => Set<Image>();
    public DbSet<Comment> Comments => Set<Comment>();
    public DbSet<Cart> Carts => Set<Cart>();
    public DbSet<CartItem> CartItems => Set<CartItem>();
    public DbSet<Order> Orders => Set<Order>();
    public DbSet<OrderItem> OrderItems => Set<OrderItem>();

    // Model oluşmadan önce "genel kurallar" (convention) tanımlanır.
    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        // Bütün decimal alanlar decimal(18,2) olsun. Tek tek [Precision] yazmaya gerek kalmaz.
        configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
    }

    // Fluent API: Attribute ile yapılamayan ayarlar burada.
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder); // Identity tablolarının ayarları için ŞART

        // Çoka-çok ilişki: ara tabloyu EF Core oluşturur, biz sadece adını veriyoruz.
        builder.Entity<Product>()
            .HasMany(p => p.Categories)
            .WithMany(c => c.Products)
            .UsingEntity(j => j.ToTable("ProductCategories"));

        // Aynı isimde iki kategori olamaz.
        builder.Entity<Category>().HasIndex(c => c.Name).IsUnique();

        // Her kullanıcının tek sepeti olur.
        builder.Entity<Cart>().HasIndex(c => c.UserId).IsUnique();

        // Siparişlerde geçen bir ürün silinemez (sipariş geçmişi bozulmasın).
        builder.Entity<OrderItem>()
            .HasOne(i => i.Product)
            .WithMany()
            .HasForeignKey(i => i.ProductId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.Entity<Order>().HasIndex(o => o.OrderNumber).IsUnique();
        builder.Entity<Order>().HasIndex(o => o.UserId);
    }
}
