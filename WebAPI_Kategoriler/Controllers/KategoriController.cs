using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using WebAPI_Kategoriler.Models;

namespace WebAPI_Kategoriler.Controllers
{
    public class KategoriController : ApiController
    {
        private E_TICARET_2023_MVCNETEntities db = new E_TICARET_2023_MVCNETEntities();

        // GET: api/Kategori
        public List<Kategori> GetKategoriler()
        {
            List<Kategoriler> liste = db.Kategoriler.ToList();
            List<Kategori> kategoriler = new List<Kategori>(); 
            
            kategoriler = (from x in db.Kategoriler select new Kategori { KategoriId = x.KategoriId, KategoriAdi = x.KategoriAdi }).ToList();

            return kategoriler;
        }

        // GET: api/Kategori/5
        [ResponseType(typeof(Kategoriler))]
        public IHttpActionResult GetKategoriler(int id)
        {
            Kategoriler kategoriler = db.Kategoriler.Find(id);
            if (kategoriler == null)
            {
                return NotFound();
            }
            Kategori kategori = new Kategori() { KategoriId = kategoriler.KategoriId, KategoriAdi = kategoriler.KategoriAdi };

            return Ok(kategori);
        }

        // PUT: api/Kategori/5
        [ResponseType(typeof(void))]
        public IHttpActionResult PutKategoriler(Kategoriler kategoriler)
        {
            if (!ModelState.IsValid)//model geçersizse
            {
                return BadRequest(ModelState);
            }

            //if (id != kategoriler.KategoriId)
            //{
            //    return BadRequest();
            //}

            db.Entry(kategoriler).State = EntityState.Modified;//burada aslında update işlmei yapılcağını söylüyor.

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!KategorilerExists(kategoriler.KategoriId ))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Kategori
        [ResponseType(typeof(Kategoriler))]
        public IHttpActionResult PostKategoriler(Kategoriler kategoriler)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Kategoriler.Add(kategoriler);
            db.SaveChanges();

            //return CreatedAtRoute("DefaultApi", new { id = kategoriler.KategoriId }, kategoriler);
            return Ok(); //yukarıda yazılan kod yerine bunu yapmak da aynı şey 200 döndürmüş oluyor.Sadece postta ve putta yazabilirm.
        }

        // DELETE: api/Kategori/5
        [ResponseType(typeof(Kategoriler))]
        public IHttpActionResult DeleteKategoriler(int id)
        {
            Kategoriler kategoriler = db.Kategoriler.Find(id);
            if (kategoriler == null)
            {
                return NotFound();
            }

            db.Kategoriler.Remove(kategoriler);
            db.SaveChanges();

            return Ok(kategoriler);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool KategorilerExists(int id)
        {
            return db.Kategoriler.Count(e => e.KategoriId == id) > 0;
        }
    }
}