using ETICARET.DataAccess.Concrete.EfCore;
using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.DataAccess.Seed
{
    public static class Beyza
    {
       public static async Task SeededAsync(EticaretContext context)
        {
            var telefon = new Category { Name="Telefon" };
            Product[] products = [
              CatalogSeed.Create(name:"Apple iPhone 17 Pro Max 256 GB Derin Mavi", 123_999, stock: 12, telefon, isHome: true,
                description:"6,9 inç Super Retina XDR ProMotion ekran\nA19 Pro çip\n48 MP Pro kamera sistemi",
                "iphone-17-pro-max-1.jpg", "iphone-17-pro-max-2.jpg", "iphone-17-pro-max-3.jpg", "iphone-17-pro-max-4.jpg"),
              CatalogSeed.Create(name:"Apple iPhone 17 Pro Max 256 GB Derin Mavi", 123_999, stock: 12, telefon, isHome: true,
                description:"6,9 inç Super Retina XDR ProMotion ekran\nA19 Pro çip\n48 MP Pro kamera sistemi",
                "iphone-17-pro-max-1.jpg", "iphone-17-pro-max-2.jpg", "iphone-17-pro-max-3.jpg", "iphone-17-pro-max-4.jpg"),
           ];
            context.Products.AddRange(products);
           await context.SaveChangesAsync();
        }
    }
}
