﻿using Kurumsal.Models.Sınıflar;
using System;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Helpers;
using System.Web.Mvc;

namespace Kurumsal.Controllers
{
    public class HizmetController : Controller
    {
        private KurumsalDB db = new KurumsalDB();

        #region Listeleme

        public ActionResult Index()
        {
            db.Configuration.LazyLoadingEnabled = false;
            var hizmet = db.Hizmet.Include(h => h.AltKategori);
            return View(hizmet.ToList().OrderByDescending(x => x.HizmetId));
        }

        #endregion Listeleme

        #region Ürün Ekleme

        public ActionResult Create()
        {
            ViewBag.HizmetKategoriId = new SelectList(db.AltKategori, "ID", "KategoriAdi");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Hizmet h, HttpPostedFileBase ResimURL, FormCollection form)
        {
            h.AltKategori_ID = int.Parse(form["HizmetKategoriId"].ToString());
            if (ResimURL != null)
            {
                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(ResimURL.FileName);
                string logoname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Save("~/Uploads/Hizmet/" + logoname);
                h.ResimURL = "/Uploads/Hizmet/" + logoname;
            }
            db.Hizmet.Add(h);
            db.SaveChanges();

            TempData["create"] = "Ürün ekleme işlemi başarılı";
            return RedirectToAction("Index");
        }

        #endregion Ürün Ekleme

        #region Ürün Düzenleme

        public ActionResult Edit(int id)
        {
            if (id == 0)
            {
                return HttpNotFound();
            }
            var b = db.Hizmet.Where(x => x.HizmetId == id).SingleOrDefault();
            if (b == null)
            {
                return HttpNotFound();
            }
            ViewBag.HizmetKategoriId = new SelectList(db.HizmetKategori, "ID", "KategoriAdi", b.AltKategori);
            return View(b);
        }

        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        [HttpPost]
        public ActionResult Edit(Hizmet m, string renk, HttpPostedFileBase ResimURL, string Renk)
        {
            var mkl = db.Hizmet.Where(x => x.HizmetId == m.HizmetId).SingleOrDefault();

            if (ResimURL != null)
            {
                if (System.IO.File.Exists(Server.MapPath(mkl.ResimURL)))
                {
                    System.IO.File.Delete(Server.MapPath(mkl.ResimURL));
                }
                WebImage img = new WebImage(ResimURL.InputStream);
                FileInfo imginfo = new FileInfo(ResimURL.FileName);

                string logoname = Guid.NewGuid().ToString() + imginfo.Extension;
                img.Save("~/Uploads/Hizmet/" + logoname);
                mkl.ResimURL = "/Uploads/Hizmet/" + logoname;

                mkl.ResimURL = "/Uploads/Hizmet/" + logoname;
            }

            mkl.Baslik = m.Baslik;
            mkl.RenkAdi = m.RenkAdi;
            mkl.UrunKodu = m.UrunKodu;
            mkl.Icerik = m.Icerik;
            mkl.AltKategori_ID = m.AltKategori_ID;
            db.SaveChanges();
            TempData["edit"] = "Ürün güncelleme işlemi başarılı";

            return RedirectToAction("Index", "Hizmet");
        }

        #endregion Ürün Düzenleme

        #region Ürün Silme

        public ActionResult Delete(int id)
        {
            var hizmet = db.Hizmet.Where(x => x.HizmetId == id).SingleOrDefault();
            if (System.IO.File.Exists(Server.MapPath(hizmet.ResimURL)))
            {
                System.IO.File.Delete(Server.MapPath(hizmet.ResimURL));
            }
            db.Hizmet.Remove(hizmet);
            db.SaveChanges();
            TempData["delete"] = "Ürün silme işlemi başarılı";
            return RedirectToAction("Index", "Hizmet");
        }

        #endregion Ürün Silme
    }
}