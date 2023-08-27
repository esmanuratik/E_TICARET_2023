using E_TICARET_2023.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace E_TICARET_2023.Controllers
{
    public class HomeController : Controller
    {
        E_TICARET_2023_MVCNETEntities db=new E_TICARET_2023_MVCNETEntities();
        public ActionResult Index()
        {
            ViewBag.KategoriListesi=db.Kategoriler.ToList();
            ViewBag.UrunListesi=db.Ürünler.ToList();
            return View();
        }

        public ActionResult About()
        {
           
        
            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult UrunDetay(int id)
        {
            ViewBag.KategoriListesi = db.Kategoriler.ToList();

            return View(db.Ürünler.Find(id));
        }
        public ActionResult Kategori(int id)
        {
            ViewBag.KategoriListesi = db.Kategoriler.ToList();
            ViewBag.Kategori = db.Kategoriler.Find(id).KategoriAdi;
            return View(db.Ürünler.Where(x=>x.KategoriId==id).ToList());
        }
             
      
    }
}