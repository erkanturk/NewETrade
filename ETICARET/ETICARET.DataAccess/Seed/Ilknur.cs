using ETICARET.DataAccess.Concrete.EfCore;
using ETICARET.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace ETICARET.DataAccess.Seed
{
    public static class Ilknur
    {
        public static async Task SeededAsync(EticaretContext context)
        {
            var beyazesya = new Category { Name = "Beyaz Eşya" };
            Product[] products = [
              CatalogSeed.Create(name:"Arçelik 10140 PMF\r\n10 Kg Çamaşır Makinesi", 76_099, stock: 12, category:beyazesya , isHome: true,
                description:"Prosmart Inverter Motor\nFiberCatcher®\nMaksimum Sıkma Devri 1400 rpm",
                "arcelik.camasirmakinesi.1.webp", "arcelik.camasirmakinesi.2.webp", "arcelik.camasirmakinesi.3.webp"),

              CatalogSeed.Create(name:"Arçelik 274532 EB\r\nAlttan Donduruculu Buzdolabı", 85_399 , stock: 10, beyazesya, isHome: false,
                description:"VitaminZone Teknolojisi\nToplam Hacim (L)532 L\nSoğutucu Aydınlatması",
                "arcelik.buzdolabi.1.webp", "arcelik.buzdolabi.2.webp", "arcelik.buzdolabi.3.webp"),

              CatalogSeed.Create(name:"LG No Frost Buzdolabı \r\n 462 Litre Kapasite",50_499 , stock: 13, beyazesya, isHome: false,
                description:"DoorCooling+™\nNatureFRESH™\nKusursuz Dış Tasarım",
                "lg.buzdolabi.1.jpg", "lg.buzdolabi.2.jpg", "lg.buzdolabi.3.jpg"),

              CatalogSeed.Create(name:"ALTUS AL CM 91050 M \r\n Çamaşır Makinesi ", 19_999 , stock: 7, beyazesya, isHome: false,
                description:"A Enerji Sınıfı\n9 kg \n1000 Devir",
                "altus.camasirmakinesi.1.webp", "altus.camasirmakinesi.2.webp", "altus.camasirmakinesi.3.webp"),

              CatalogSeed.Create(name:"HOOVER HLEH10A2TCEX-17 \r\n Isı Pompalı Kurutma Makinesi", 17_999 , stock: 20, beyazesya, isHome: true,
                description:"10 Kg\nWi-Fi Bağlantılı \nIsı Pompalı",
                "kurutma1.webp", "kurutma2.webp", "kurutma3.webp"),

              CatalogSeed.Create(name:"BOSCH SMS26DI00T \r\n Bulaşık Makinesi", 65_399 , stock: 10, beyazesya, isHome: false,
                description:" D Enerji Sınıfı\n6 Program\nSoğutucu Aydınlatması\nEcoSilence",
                "bulasik1.webp", "bulasik2.webp", "bulasik3.webp"),

              CatalogSeed.Create(name:"Vestel SO 6004 S DG\r\n Set Üstü Ocak", 11_540 , stock: 20, beyazesya, isHome: true,
                description:"4 Gözü Gazlı \nGaz Söndürme Emniyeti\nDüğmeden Ateşleme\r\n",
                "ocak1.jpeg", "ocak2.jpeg", "ocak3.jpeg"),

              CatalogSeed.Create(name:"Vestel AD-63330 S \r\n Ankastre Davlumbaz", 14_429 , stock: 8, beyazesya, isHome: false,
                description:"Duvar Tipi\nDokunmatik Kontrol \n2x3W LED Ocak Aydınlatması",
                "davlumbaz1.jpeg", "davlumbaz2.jpeg", "davlumbaz3.jpeg"),

              CatalogSeed.Create(name:"Vestel SF 8400  \r\nSolo Fırın",28_599 , stock: 7, beyazesya, isHome: false,
                description:"Elektroturbo Fırın\n4 Gözü Gazlı\nBörek ve Yoğurt Yapma Fonksiyonu",
                "firin1.jpeg", "firin2.jpeg", "firin3.jpeg"),

              CatalogSeed.Create(name:"Vestel CDM701\r\nDikey Derin Dondurucu",32_999, stock: 18, beyazesya, isHome: false,
                description:"7 Bölme\n259 Litre Net Hacim\nDeğiştirilebilir Kapı Açılış Yönü",
                "derindondurucu1.jpeg", "derindondurucu2.jpeg", "derindondurucu3"),];
            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}
