using E_TICARET_2023.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace E_TICARET_2023.Controllers
{
    public class SepetController : Controller
    {

        E_TICARET_2023_MVCNETEntities db = new E_TICARET_2023_MVCNETEntities();
        
        public ActionResult Index()
        {
            //Kimin Sepeti kullancı atmalıyım
            string userId = User.Identity.GetUserId();

            var sepet = db.Sepet.Where(x => x.KullaniciId == userId);
            return View(db.Sepet.ToList());
        }
        public ActionResult SepeteEkle(int UrunId,int adet)//sepetekle view olmayacak onun yerine sepet ındexin view olacak
        {//sepetin databse de 4 tane bilgisi var onu göndermeliyim.

            string userId=User.Identity.GetUserId(); 
            Ürünler urun=db.Ürünler.Find(UrunId);

            Sepet sepettekiurun=db.Sepet.FirstOrDefault(x=>x.UrunId==UrunId && x.KullaniciId==userId);

            if (sepettekiurun==null)
            {
                Sepet sepet = new Sepet()
                {
                    KullaniciId = userId,
                    UrunId = UrunId,
                    Adet = adet,
                    ToplamTutar = adet * urun.UrunFiyati,

                };
                db.Sepet.Add(sepet);
             
            }
            else
            {
                sepettekiurun.Adet += adet;
                sepettekiurun.ToplamTutar=sepettekiurun.Adet * urun.UrunFiyati; 
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }
        public ActionResult SepetGuncelle(int id,int adet)
        {
            Sepet sepet = db.Sepet.Find(id);
            if (sepet == null)
            {
                return HttpNotFound();
            }
            Ürünler urun = db.Ürünler.Find(sepet.UrunId);

            sepet.Adet = adet;
            sepet.ToplamTutar=sepet.Adet * urun.UrunFiyati;
            db.SaveChanges();

            return RedirectToAction("Index");
        }
        public ActionResult Sepetsil(int id)
        {
            Sepet sepet = db.Sepet.Find(id);
            if (sepet != null)
            {
                db.Sepet.Remove(sepet);
                db.SaveChanges();
            }
            return RedirectToAction("Index");
        }
    }
}