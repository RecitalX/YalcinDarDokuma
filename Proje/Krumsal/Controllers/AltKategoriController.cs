using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Kurumsal.Models.Sınıflar;

namespace Kurumsal.Controllers
{
    public class AltKategoriController : Controller
    {
        private KurumsalDB db = new KurumsalDB();

        // GET: AltKategori
        public ActionResult Index()
        {
            var altKategori = db.AltKategori.Include(a => a.HizmetKategori);
            return View(altKategori.ToList());
        }

 

        public ActionResult Create()
        {
            ViewBag.HizmetKategoriId = new SelectList(db.HizmetKategori, "HizmetKategoriId", "HizmetKategoriAdi");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,KategoriAdi,HizmetKategoriId")] AltKategori altKategori)
        {
            if (ModelState.IsValid)
            {
                db.AltKategori.Add(altKategori);
                db.SaveChanges();
                TempData["create"] = "Alt kategori ekleme işlemi başarılı";
                return RedirectToAction("Index");
            }

            ViewBag.HizmetKategoriId = new SelectList(db.HizmetKategori, "HizmetKategoriId", "HizmetKategoriAdi", altKategori.HizmetKategoriId);
            return View(altKategori);
        }

        // GET: AltKategori/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AltKategori altKategori = db.AltKategori.Find(id);
            if (altKategori == null)
            {
                return HttpNotFound();
            }
            ViewBag.HizmetKategoriId = new SelectList(db.HizmetKategori, "HizmetKategoriId", "HizmetKategoriAdi", altKategori.HizmetKategoriId);
            return View(altKategori);
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,KategoriAdi,HizmetKategoriId")] AltKategori altKategori)
        {
            if (ModelState.IsValid)
            {
                db.Entry(altKategori).State = EntityState.Modified;
                db.SaveChanges();
                TempData["edit"] = "Alt kategori güncelleme işlemi başarılı";

                return RedirectToAction("Index");
            }
            ViewBag.HizmetKategoriId = new SelectList(db.HizmetKategori, "HizmetKategoriId", "HizmetKategoriAdi", altKategori.HizmetKategoriId);
            return View(altKategori);
        }


        public ActionResult Delete(int? id)
        {
            try
            {
                if (id == null)
                {
                    return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
                }
                AltKategori hizmetKategori = db.AltKategori.Find(id);
                if (hizmetKategori == null)
                {
                    return HttpNotFound();
                }
                db.AltKategori.Remove(hizmetKategori);
                db.SaveChanges();
                TempData["delete"] = "Alt kategori silme işlemi başarılı";
            }
            catch (Exception ex)
            {
                TempData["DeleteMessage"] = "failed : " + ex.Message;
                TempData["DeleteMessage1"] = "Hata : " + "Bu kategoriyi kullanan ürün varken bu kategoriyi silemezsiniz.";
            }
            //TempData["delete"] = "Kategori silme işlemi başarılı";
            return RedirectToAction("Index", "AltKategori");
        }
    }
}
