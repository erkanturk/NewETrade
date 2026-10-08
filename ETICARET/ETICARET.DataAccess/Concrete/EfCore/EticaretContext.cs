using ETICARET.Entities;
using ETICARET.Entities.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.DataAccess.Concrete.EfCore
{
    public class EticaretContext(DbContextOptions<EticaretContext>options):IdentityDbContext<ApplicationUser>(options)
    {
        public DbSet<Product> Products => Set<Product>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<Image> Images => Set<Image>();
        public DbSet<Comment> Comments => Set<Comment>();
        public DbSet<Cart> Carts => Set<Cart>();
        public DbSet<CartItem> CartItems => Set<CartItem>();
        public DbSet<Order> Orders => Set<Order>();
        public DbSet<OrderItem> OrderItems => Set<OrderItem>();

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            configurationBuilder.Properties<decimal>().HavePrecision(18, 2);
        }
        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            //Çoka çok ilişki ara tablo ile ef core oluşturur biz sadece aynı adı veriyoruz.
            builder.Entity<Product>()
                .HasMany(p => p.Categories)
                .WithMany(c => c.Products).UsingEntity(j => j.ToTable("ProductCategories"));
            //Aynı isimde iki kategori bulunamaz.
            builder.Entity<Category>().HasIndex(c => c.Name).IsUnique();

            //Her kullanıcının tek sepeti olacak
            builder.Entity<Cart>().HasIndex(c => c.UserId).IsUnique();

            //Siparişlerde geçen bir ürün silinemez (sipariş geçmişi bozulmasın)
            builder.Entity<OrderItem>()
                .HasOne(i => i.Product)
                .WithMany()
                .HasForeignKey(i => i.ProductId)
                .OnDelete(DeleteBehavior.Restrict);
            builder.Entity<Order>().HasIndex(o => o.OrderNumber).IsUnique();
            builder.Entity<Order>().HasIndex(o => o.UserId);
        }
    }
}
