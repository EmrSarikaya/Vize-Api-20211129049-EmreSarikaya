//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace uyg01.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Kayit
    {
        public string kayitId { get; set; }
        public string kayitKategoriId { get; set; }
        public string kayitIlanId { get; set; }
    
        public virtual Ilan Ilan { get; set; }
        public virtual Kategori Kategori { get; set; }
    }
}
