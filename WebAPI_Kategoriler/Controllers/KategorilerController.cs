using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Mvc;
using WebAPI_Kategoriler.Models;

namespace WebAPI_Kategoriler.Controllers
{
    public class KategorilerController : ApiController
    {
        E_TICARET_2023_MVCNETEntities db=new E_TICARET_2023_MVCNETEntities();
        public List<Kategori> Get()
        {
            List<Kategoriler> liste = db.Kategoriler.ToList();
            List<Kategori> kategoriler = new List<Kategori>();

            //foreach (var item in liste)
            //{
            //    kategoriler.Add(new Kategori()
            //    {
            //        KategoriId = item.KategoriId,   
            //        KategoriAdi = item.KategoriAdi, 
            //    });
            //}

            //foreach ile yaptığımın aynısı Linq sorgusu ile yapılıyor
            kategoriler = (from x in db.Kategoriler select new Kategori { KategoriId = x.KategoriId, KategoriAdi = x.KategoriAdi }).ToList();

            return kategoriler;
        }
        public Kategori Get(int id)
        {
            Kategoriler kategoriler= db.Kategoriler.Find(id);
            Kategori kategori= new Kategori() { KategoriId=kategoriler.KategoriId,KategoriAdi=kategoriler.KategoriAdi};
            return kategori;
        }
    }
}
