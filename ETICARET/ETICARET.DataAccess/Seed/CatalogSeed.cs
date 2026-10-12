using ETICARET.DataAccess.Concrete.EfCore;
using ETICARET.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.DataAccess.Seed
{
    public static class CatalogSeed
    {
        public static async Task SeedAsync(EticaretContext context)
        {
            if (await context.Categories.AnyAsync()) return;

            var telefon = new Category { Name="Telefon" };
            var bilgisayar = new Category { Name="Bilgisayar" };
            var elektronik = new Category { Name="Elektronik" };
            var oyun = new Category { Name="Oyun ve Aksesuar" };
            var beyazEşya = new Category { Name="Beyaz Eşya" };
            var evMutfak = new Category { Name="Ev ve Mutfak" };


            //123_999 123999 okunabilirlik açısından _ yapılır.
           // Product[] products = [
           //      Create(name:"Apple iPhone 17 Pro Max 256 GB Derin Mavi", 123_999, stock: 12, telefon, isHome: true,
           //     description:"6,9 inç Super Retina XDR ProMotion ekran\nA19 Pro çip\n48 MP Pro kamera sistemi",
           //     "iphone-17-pro-max-1.jpg", "iphone-17-pro-max-2.jpg", "iphone-17-pro-max-3.jpg", "iphone-17-pro-max-4.jpg"),
           //];
           if (context.Products.Count()<20)
            {
                Beyza.SeededAsync(context);
            }
            context.Categories.AddRange(telefon, bilgisayar, elektronik, oyun, beyazEşya, evMutfak);
           
            await context.SaveChangesAsync();
        }
       
        public static Product Create(string name, decimal price, int stock, Category category, bool isHome,
            string description, params string[] images
            ) => new()
            {
                Name = name,
                Price = price,
                Stock = stock,
                IsHome = isHome,
                Description = description,
                Categories= [category],
                Images= [.. images.Select(url=>new Image { ImageUrl=url})]
            };
    }
}
