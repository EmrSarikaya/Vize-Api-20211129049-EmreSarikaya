using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace uyg01.ViewModel
{
    public class KayitModel
    {
        public string kayitId { get; set; }
        public string kayitKategoriId { get; set; }
        public string kayitIlanId { get; set; }
        public IlanModel ilanBilgi { get; set; }
        public KategoriModel kategoriBilgi { get; set; }
    }
}