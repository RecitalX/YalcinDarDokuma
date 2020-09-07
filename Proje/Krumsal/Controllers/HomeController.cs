using Kurumsal.Models.Sınıflar;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace Kurumsal.Controllers
{
    public class HomeController : Controller
    {
        private KurumsalDB db = new KurumsalDB();

        #region Anasayfa

        [Route("")]
        [Route("Anasayfa")]
        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View();
        }

        #endregion Anasayfa

        #region Ürünler

        [Route("Ürünler")]
        public ActionResult Urun(int Sayfa = 1)
        {
            List<HizmetKategori> kategorilistesi = db.HizmetKategori.Where(x => x.HizmetKategoriId > 0).OrderByDescending(x => x.HizmetKategoriId).ToList();
            ViewBag.Kategorilerim = kategorilistesi;
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hizmet.Include("HizmetKategori").OrderByDescending(x => x.HizmetId).ToPagedList(Sayfa, 40));
        }

        #endregion Ürünler

        #region Ürün Detay

        [Route("ÜrünPost/{Baslik}-{id:int}")]
        public ActionResult UrunDetay(int id)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var u = db.Hizmet.Include("AltKategori").Where(x => x.HizmetId == id).SingleOrDefault();
            return View(u);
        }

        #endregion Ürün Detay

        #region Kategoriye Ait Ürünler

        //Kategoriye Ait Hizmetler
        [Route("ÜrünPost/{HizmetKategoriAdi}/{id:int}")]
        public ActionResult KategoriyeAitUrunler(int id)
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var u = db.Hizmet.Include("AltKategori").OrderByDescending(x => x.HizmetId).Where(x => x.AltKategori.ID == id).ToList();
            TempData["UrunYok"] = "Bu kategoriye ait ürün bulunmamaktadır.";
            return View(u);
        }

        #endregion Kategoriye Ait Ürünler
        
        public ActionResult KategoriList()
        {
            MultipleModels models = new MultipleModels();

            List<HizmetKategori> duyurular = db.HizmetKategori.ToList();
            List<AltKategori> basvurular = db.AltKategori.ToList();

            models.Tub = new Tuple<List<HizmetKategori>, List<AltKategori>>(duyurular, basvurular);

            return View(models);
        }
   
        #region Hakkımızda

        [Route("Hakkımızda")]
        public ActionResult Hakkimizda()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View(db.Hakkimizda.ToList());
        }

        #endregion Hakkımızda

        #region En Son Eklenen Ürünler

        public ActionResult EnsonEklenenUruler()
        {
            var urun = db.Hizmet.OrderByDescending(x => x.HizmetId).Take(9);
            return View(urun);
        }

        #endregion En Son Eklenen Ürünler

        #region Arama Yap

        [Route("AramaSayfası")]
        public ActionResult AramaYap(string aranan)
        {
            List<HizmetKategori> kategorilistesi = db.HizmetKategori.Where(x => x.HizmetKategoriId > 0).OrderByDescending(x => x.HizmetKategoriId).ToList();
            ViewBag.Kategorilerim = kategorilistesi;
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            var urun = from d in db.Hizmet select d;
            if (!string.IsNullOrEmpty(aranan))
            {
                urun = urun.Where(x => x.Baslik.Contains(aranan) || x.UrunKodu.Contains(aranan) || x.Icerik.Contains(aranan));
                if (urun.Count() == 0)
                {
                    ViewBag.NotFound = "Aramanız eşleşen bir sonuç bulunamadı.";
                    return View(urun);
                }
            }
            return View(urun.ToList());
        }

        #endregion Arama Yap

        #region Mesaj Gönderme

        [HttpGet]
        [Route("İletişim")]
        public ActionResult Mesaj()
        {
            ViewBag.Kimlik = db.Kimlik.SingleOrDefault();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Mesaj([Bind(Include = "ID,AdSoyad,Telefon,Konu,İleti")] Mesaj mesaj)
        {
            if (ModelState.IsValid)
            {

                //string massageDate = DateTime.Now.ToString("HH:mm");
                //mesaj.saat = massageDate;
                //KODLAR BUNLAR

                mesaj.saat = DateTime.Now.Hour;
                db.Mesaj.Add(mesaj);
                db.SaveChanges();
            }
            TempData["Basarılı"] = "Mesaj gönderme işlemi başarılı";
            return RedirectToAction("Mesaj", "Home");
        }

        #endregion Mesaj Gönderme

        #region İletşim

        public ActionResult Iletisim()
        {
            var iletisim = db.Iletisim.ToList();
            return PartialView(iletisim);
        }

        #endregion İletşim
    }
}