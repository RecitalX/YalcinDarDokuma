using Kurumsal.Models.Sınıflar;
using System.Collections.Generic;
using System.Data.Entity;
using System.Web.Helpers;

namespace Kurumsal.Models.DB_Contect
{
    public class Initializer : DropCreateDatabaseIfModelChanges<KurumsalDB>
    {
        protected override void Seed(KurumsalDB context)
        {
            var kimlikler = new List<Kimlik>()
            {
                new Kimlik(){Title="Test verisi",Description="Test verisi",Keywords="Test verisi",LogoURL="1.jpg"}
            };
            foreach (var item in kimlikler)
            {
                context.Kimlik.Add(item);
            }
            context.SaveChanges();

            var hakkimizda = new List<Hakkimizda>()
            {
                new Hakkimizda(){Aciklama="Test Verisi"}
            };
            foreach (var item in hakkimizda)
            {
                context.Hakkimizda.Add(item);
            }
            context.SaveChanges();


            var iletişimbilgisi = new List<Iletisim>()
            {
                new Iletisim(){Telefon="Test verisi",Mail="Test verisi",Adres="Test verisi"}
            };
            foreach (var item in iletişimbilgisi)
            {
                context.Iletisim.Add(item);
            }
            context.SaveChanges();

            Admin admin = new Admin()
            {
                Eposta = "isemihbl@gmail.com",
                Sifre = Crypto.HashPassword("123"),
                Yetki = "Admin"
            };
            context.Admin.Add(admin);
            context.SaveChanges();

            base.Seed(context);
        }
    }
}