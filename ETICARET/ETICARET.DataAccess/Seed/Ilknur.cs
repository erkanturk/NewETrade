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
            var evMutfak = new Category { Name = "Ev ve Mutfak" };
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
                description:"4 Gözü Gazlı \nGaz Söndürme Emniyeti\nDüğmeden Ateşleme",
                "ocak1.jpeg", "ocak2.jpeg", "ocak3.jpeg"),

              CatalogSeed.Create(name:"Vestel AD-63330 S \r\n Ankastre Davlumbaz", 14_429 , stock: 8, beyazesya, isHome: false,
                description:"Duvar Tipi\nDokunmatik Kontrol \n2x3W LED Ocak Aydınlatması",
                "davlumbaz1.jpeg", "davlumbaz2.jpeg", "davlumbaz3.jpeg"),

              CatalogSeed.Create(name:"Vestel SF 8400  \r\nSolo Fırın",28_599 , stock: 7, beyazesya, isHome: false,
                description:"Elektroturbo Fırın\n4 Gözü Gazlı\nBörek ve Yoğurt Yapma Fonksiyonu",
                "firin1.jpeg", "firin2.jpeg", "firin3.jpeg"),

              CatalogSeed.Create(name:"Vestel CDM701\r\nDikey Derin Dondurucu",32_999, stock: 18, beyazesya, isHome: false,
                description:"7 Bölme\n259 Litre Net Hacim\nDeğiştirilebilir Kapı Açılış Yönü",
                "derindondurucu1.jpeg", "derindondurucu2.jpeg", "derindondurucu3"),


              CatalogSeed.Create(name: "Arçelik Imperium® Slim 3.0 SDI 9524\r\nŞarjlı Dikey Süpürge", 29_999, stock: 17, category: evMutfak, isHome: true,
                description: "Yeşil Led Teknolojisi\n Toz Haznesi Kapasitesi 0.5 L \n Kablosuz Çalışma Süresi 80 min",
                "dikeysupurge1.webp", "dikeysupurge2.webp", "dikeysupurge3.webp"),

              CatalogSeed.Create(name: "Arçelik EM 9595 Imperium Barista®\r\nTam Otomatik Espresso Makinesi", 29_999 , stock: 9, evMutfak, isHome: false,
                description: "Süt Köpüğü Ayarı\nKahve Çeşidi Sayısı 50\n13 Kahve Öğütücü Kademesi ",
                "arcelik.buzdolabi.1.webp", "arcelik.buzdolabi.2.webp", "arcelik.buzdolabi.3.webp"),

              CatalogSeed.Create(name: "Arçelik Aria™ MasterPro BKU 9130\r\n Kazanlı Ütü", 50_499, stock: 13, evMutfak, isHome: false,
                description: "Güç 3000 W\nSuperGlide Seramik Taban\nSürekli Buhar 165 gr/dk",
                "utu1.webp", "utu2.webp", "utu3.webp"),

              CatalogSeed.Create(name: "Bosch Serie 8 \r\n Mutfak Makinesi 1800 W Gümüş, Siyah ", 46_100, stock: 5, evMutfak, isHome: false,
                description: "Güç:  1800 W\nHız ayarı sayısı:7\nMaksimum hamur yoğurma miktarı : 3.6 kg",
                "mutfak1.webp", "mutfak2.webp", "mutfak3.webp"),

              CatalogSeed.Create(name: "BOSCH \r\n Tost Makinesi Gümüş", 8_600, stock: 20, beyazesya, isHome: true,
                description: "Sıcaklık ayarı : 5 & Üst plaka için ekstra düğme \nIzgara plakası : Alüminyum pres döküm\n Overheating protection",
                "tost1.webp", "tost2.webp", "tost3.webp"),

              CatalogSeed.Create(name: "Arzum OK004 Okka Minio \r\n Türk Kahvesi Makinesi ", 3_299, stock: 20, evMutfak, isHome: false,
                description: " Makinesi Taşma Önleyici Sistem\n 4 Fincan Kapasiteli\nBakır",
                "kahve1.jpeg", "kahve2.jpeg", "kahve3.jpeg"),

              CatalogSeed.Create(name: "Arzum AR1192 Shake'N Take Duo \r\nKişisel Blender", 4_399, stock: 6, evMutfak, isHome: false,
                description: " 550 W motor gücü\n4 bıçaklı \nBuz Kırma\nBPA içermez",
                "blender1.jpeg", "blender2.jpeg", "blender3.jpeg"),

              CatalogSeed.Create(name: "Arzum AR5166 Revolution BLDC Ionic\r\n Hava Üflemeli Saç Şekillendirici", 14_429, stock: 8, evMutfak, isHome: false,
                description: " 6 Farklı Başlıklı\n 1500W \n Ion teknolojisi ",
                "sac1.jpeg", "sac2.jpeg", "sac3.jpeg"),

              CatalogSeed.Create(name: "Xiaomi Robot Vacuum S40  \r\n Robot Süpürge", 13_299, stock:14, evMutfak, isHome: true,
                description: "10.000 Pa emiş gücüne sahip kasırga gibi fan üfleyici\n5.200 mAh uzun pil ömrü\nBAkıllı entegre süpürme ve paspaslama",
                "robot1.jpeg", "robot2.jpeg", "robot3.jpeg","robot4.jpeg"),

              CatalogSeed.Create(name: "Smeg Cream Klf03creuKettle\r\nKettle", 14_993, stock: 7, evMutfak, isHome: false,
                description: "“Yumuşak açılır” kapak sistemi \n100 dereceye ulaştığında otomatik kapanma\nPaslanmaz çelik gövde",
                "kettle1.jpeg", "kettle2.jpeg", "kettle3.jpeg"),];



            context.Products.AddRange(products);
            await context.SaveChangesAsync();
        }
    }
}


