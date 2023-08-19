using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Mvc;
using E_TICARET_2023.Models;
using Newtonsoft.Json;

namespace E_TICARET_2023.Controllers
{
    [Authorize(Roles ="admin")]//Role ne tanımlandıysa o yazıldı database de(Asp.NET RolesTable)
    public class KategorilerController : Controller
    {
        private E_TICARET_2023_MVCNETEntities db = new E_TICARET_2023_MVCNETEntities();
        HttpClient client = new HttpClient();   //apiyi vermek için böyle bir nesne türetmeliyim
        // GET: Kategoriler
        public ActionResult Index()
        {//Api yi burada kullanıyorum.
            List<Kategoriler> liste = db.Kategoriler.ToList();

            client.BaseAddress = new Uri("https://localhost:44329/api/");
            var cevap=client.GetAsync("Kategori");//api de en son yaptığımız controller 
            cevap.Wait();

            if (cevap.Result.IsSuccessStatusCode)//api çalıştığında json data çalışır.Gelen datayı önce okkuyup sonra çalıştırmalıyız.
            {
                var data = cevap.Result.Content.ReadAsStringAsync();//read ile içeriği oku
                data.Wait();//artık elimde data var data=json
                liste = JsonConvert.DeserializeObject<List<Kategoriler>>(data.Result);             
            }

            return View(liste);
            //return View(db.Kategoriler.ToList()); //bu şekilde yaptığımız kategori getirme işlemini api ile getirdik.
        }

        // GET: Kategoriler/Details/5
        public ActionResult Details(int? id) 
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = KategoriBul(id.Value);/*db.Kategoriler.Find(id.Value)*/;//apiden çekmek için id göndererek böyle yzadık kategoribul metotu yazdık karışmasın diye.
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }
        private Kategoriler KategoriBul(int id)//Api den çekeyim diye metodu burada yazdım.Burada id vererek tek bir kategori çekmiş olduk
        {
            Kategoriler kategori = null;
            //kategori= db.Kategoriler.Find(id.Value);

            client.BaseAddress = new Uri("https://localhost:44329/api/");
            var cevap = client.GetAsync("Kategori/"+id.ToString());//api de en son yaptığımız controller 
            cevap.Wait();

            if (cevap.Result.IsSuccessStatusCode)
            {
                var data = cevap.Result.Content.ReadAsStringAsync();//ReadAsAsync kullansaydım kategori=data.Result diyip bitirebilirdim aynı şeye tekabül ediyor.
                data.Wait();
                kategori=JsonConvert.DeserializeObject<Kategoriler>(data.Result);   
            }

            return kategori;
        }

        // GET: Kategoriler/Create
        public ActionResult Create()
        {
            return View();
        }

        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "KategoriId,KategoriAdi")] Kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                //db.Kategoriler.Add(kategoriler);
                //db.SaveChanges();
                //return RedirectToAction("Index"); 

                //bu işlemleri api ile yapacağım.
                client.BaseAddress = new Uri("https://localhost:44329/api/");
                var cevap=client.PostAsJsonAsync<Kategoriler>("Kategori",kategoriler);
                cevap.Wait();
                if (cevap.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");//başarılıysa ındexe yönlendirebilir yani post işlmei gerçekleşebilir.
                }
            }

            return View(kategoriler);
        }

        // GET: Kategoriler/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = KategoriBul(id.Value);/*db.Kategoriler.Find(id);*/
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "KategoriId,KategoriAdi")] Kategoriler kategoriler)
        {
            if (ModelState.IsValid)
            {
                //db.Entry(kategoriler).State = EntityState.Modified;
                //db.SaveChanges();
                //return RedirectToAction("Index");

                //bu işlemi api ile yapcağım.
                client.BaseAddress = new Uri("https://localhost:44329/api/");
                var cevap = client.PutAsJsonAsync<Kategoriler>("Kategori", kategoriler);
                cevap.Wait();
                if (cevap.Result.IsSuccessStatusCode)
                {
                    return RedirectToAction("Index");
                }
            }
            return View(kategoriler);
        }

        // GET: Kategoriler/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Kategoriler kategoriler = KategoriBul(id.Value);/*db.Kategoriler.Find(id);*/
            if (kategoriler == null)
            {
                return HttpNotFound();
            }
            return View(kategoriler);
        }

        // POST: Kategoriler/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            //Kategoriler kategoriler = db.Kategoriler.Find(id);
            //db.Kategoriler.Remove(kategoriler);
            //db.SaveChanges();

            //bu işlemi api ile yapıyoruz.
            client.BaseAddress = new Uri("https://localhost:44329/api/");
            var cevap = client.DeleteAsync("Kategori/" + id.ToString());
            cevap.Wait();
            if (cevap.Result.IsSuccessStatusCode)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}
