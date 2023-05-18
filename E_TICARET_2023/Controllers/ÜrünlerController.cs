using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.IO;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using E_TICARET_2023.Models;

namespace E_TICARET_2023.Controllers
{
    public class ÜrünlerController : Controller
    {
        private E_TICARET_2023_MVCNETEntities db = new E_TICARET_2023_MVCNETEntities();

        
        public ActionResult Index()
        {
            var ürünler = db.Ürünler.Include(ü => ü.Kategoriler);//her ürünün bir kategorisi var olduğundan bunları bağlamış
            return View(ürünler.ToList());
        }

        
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ürünler ürünler = db.Ürünler.Find(id);
            if (ürünler == null)
            {
                return HttpNotFound();
            }
            return View(ürünler);
        }

       
        public ActionResult Create()
        {
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi");
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Ürünler ürünler,HttpPostedFileBase UrunResim)//create view de resimi eklediğimiz kısımda verdiğimiz name i yakalamamız lazım.
        {
            if (ModelState.IsValid)
            {
                db.Ürünler.Add(ürünler);
                db.SaveChanges();

                if (UrunResim != null && UrunResim.ContentLength>0)
                {
                    string dosya = Server.MapPath("~/Resim/")+ürünler.UrunId+".jpg";
                    UrunResim.SaveAs(dosya);
                }
                return RedirectToAction("Index");
            }

            ViewBag.KategoriId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi", ürünler.KategoriId);
            return View(ürünler);
        }

      
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ürünler ürünler = db.Ürünler.Find(id);
            if (ürünler == null)
            {
                return HttpNotFound();
            }
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi", ürünler.KategoriId);
            return View(ürünler);
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Ürünler ürünler , HttpPostedFileBase UrunResim)
        {
              if (ModelState.IsValid)
            {
                db.Entry(ürünler).State = EntityState.Modified;
                db.SaveChanges();

                if (UrunResim != null && UrunResim.ContentLength > 0)
                {
                    string dosya = Server.MapPath("~/Resim/") + ürünler.UrunId + ".jpg";
                    UrunResim.SaveAs(dosya);
                }

                return RedirectToAction("Index");
            }
            ViewBag.KategoriId = new SelectList(db.Kategoriler, "KategoriId", "KategoriAdi", ürünler.KategoriId);
            return View(ürünler);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Ürünler ürünler = db.Ürünler.Find(id);
            if (ürünler == null)
            {
                return HttpNotFound();
            }
            return View(ürünler);
        }

       
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Ürünler ürünler = db.Ürünler.Find(id);
            db.Ürünler.Remove(ürünler);
            db.SaveChanges();

            string dosya = Server.MapPath("~/Resim/") + ürünler.UrunId + ".jpg";
            FileInfo file=new FileInfo(dosya);  
            if (file.Exists)
            {
                file.Delete();
            }


            return RedirectToAction("Index");
        }

    }
}
