//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace WebAPI_Kategoriler.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Sepet
    {
        public int SepetId { get; set; }
        public Nullable<int> UrunId { get; set; }
        public string KullaniciId { get; set; }
        public Nullable<int> Adet { get; set; }
        public Nullable<int> ToplamTutar { get; set; }
    
        public virtual AspNetUsers AspNetUsers { get; set; }
        public virtual Ürünler Ürünler { get; set; }
    }
}
