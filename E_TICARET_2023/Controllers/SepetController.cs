using E_TICARET_2023.Models;
using Microsoft.AspNet.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using E_TICARET_2023.Models;

namespace E_TICARET_2023.Controllers
{
    public class SepetController : Controller
    {
        // GET: Sepet
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult SepeteEkle(int UrunId,int adet)//sepetekle view olmayacak onun yerine sepet ındexin view olacak
        {//sepetin databse de 4 tane bilgisi var onu göndermeliyim.

            E_TICARET_2023_MVCNETEntities db=new E_TICARET_2023_MVCNETEntities();
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
                sepettekiurun.Adet = adet++;
                sepettekiurun.ToplamTutar=sepettekiurun.Adet * urun.UrunFiyati; 
            }

            db.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}