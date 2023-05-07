using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using uyg01.Models;
using uyg01.ViewModel;

namespace uyg01.Controllers
{
    public class ServisController : ApiController
    {
        DB01Entities db = new DB01Entities();
        SonucModel sonuc = new SonucModel();

        #region Kategori
        [HttpGet]
        [Route("api/kategoriliste")]
        public List<KategoriModel> KategoriListe() { 
        List<KategoriModel> liste = db.Kategori.Select(x => new KategoriModel()
        {
            kategoriId = x.kategoriId,
            kategoriAdi = x.kategoriAdi
        }).ToList();

        return liste;
        }

        [HttpGet]
        [Route("api/kategoribyid/{kategoriId}")]
        public KategoriModel KategoriById(string kategoriId)
        {
            KategoriModel kayit = db.Kategori.Where(s => s.kategoriId == kategoriId).Select(x => new KategoriModel() {
                kategoriId = x.kategoriId,
                kategoriAdi = x.kategoriAdi
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/kategoriekle")]
        public SonucModel KategoriEkle(KategoriModel model)
        {
            if (db.Kategori.Count(s => s.kategoriAdi == model.kategoriAdi) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen kategori kayıtlıdır!";
                return sonuc;
            }
            Kategori yeni = new Kategori();
            yeni.kategoriId = Guid.NewGuid().ToString();
            yeni.kategoriAdi = model.kategoriAdi;
            db.Kategori.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Eklendi";
            return sonuc;
       }

        [HttpPut]
        [Route("api/kategoriduzenle")]
       public SonucModel KategoriDuzenle(KategoriModel model)
        {
            Kategori kayit = db.Kategori.Where(s => s.kategoriId == model.kategoriId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı!";
                return sonuc;
            }
            kayit.kategoriAdi = model.kategoriAdi;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kategorisil/{kategoriId}")]
        public SonucModel KategoriSil(string kategoriId)
        {
            Kategori kayit = db.Kategori.Where(s => s.kategoriId == kategoriId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategori Bulunamadı!";
                return sonuc;
            }

            if(db.Kayit.Count(s => s.kayitKategoriId==kategoriId) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kategoriye Kayıtlı İlan Olduğu İçin Kategori Silinemez!";
                return sonuc;
            }
            db.Kategori.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kategori Silindi";
            return sonuc;
        }
        #endregion

        #region İlan
        [HttpGet]
        [Route("api/ilanliste")]
        public List<IlanModel> IlanListe()
        {
            List<IlanModel> liste = db.Ilan.Select(x => new IlanModel()
            {
                ilanId = x.ilanId,
                ilanAd = x.ilanAd,
                ilanFiyat = x.ilanFiyat,
                ilanFoto = x.ilanFoto,
            }).ToList();
            return liste;
        }
        [HttpGet]
        [Route("api/ilanbyid/{ilanId}")]
        public IlanModel IlanById(string ilanId)
        {
            IlanModel kayit = db.Ilan.Where(s => s.ilanId == ilanId).Select(x => new IlanModel()
            {
                ilanId = x.ilanId,
                ilanAd = x.ilanAd,
                ilanFiyat = x.ilanFiyat,
                ilanFoto = x.ilanFoto,
            }).SingleOrDefault();
            return kayit;
        }

        [HttpPost]
        [Route("api/ilanekle")]
        public SonucModel IlanEkle(IlanModel model)
        {
            if (db.Ilan.Count(s => s.ilanAd == model.ilanAd) > 0)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Girilen İlan Kayıtlıdır!";
            }

            Ilan yeni = new Ilan();
            yeni.ilanId = Guid.NewGuid().ToString();
            yeni.ilanAd = model.ilanAd;
            yeni.ilanFiyat = model.ilanFiyat;
            yeni.ilanFoto = model.ilanFoto;
            db.Ilan.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "İlan Eklendi";
            return sonuc;
        }

        [HttpPut]
        [Route("api/ilanduzenle")]
        public SonucModel IlanDuzenle(IlanModel model)
        {
            Ilan kayit = db.Ilan.Where(s => s.ilanId == model.ilanId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "İlan Bulunamadı!";
                return sonuc;
            }

            kayit.ilanAd = model.ilanAd;
            kayit.ilanFiyat = model.ilanFiyat;
            kayit.ilanFoto = model.ilanFoto;
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "İlan Düzenlendi";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/ilansil/{ilanId}")]
        public SonucModel IlanSil(string ilanId)
        {
            Ilan kayit = db.Ilan.Where(s => s.ilanId == ilanId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "İlan Bulunamadı!";
                return sonuc;
            }
            db.Ilan.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "İlan Silindi";
            return sonuc;
        }


        #endregion

        #region Kayıt
        [HttpGet]
        [Route("api/ilankategoriliste/{ilanId}")]
        public List<KayitModel> IlanKategoriListe (string ilanId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitIlanId == ilanId).Select(x => new KayitModel() {
                kayitId = x.kayitId,
                kayitKategoriId = x.kayitKategoriId,
                kayitIlanId = x.kayitIlanId,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.ilanBilgi = IlanById(kayit.kayitIlanId);
                kayit.kategoriBilgi = KategoriById(kayit.kayitKategoriId);
            }
            return liste;
        }

        [HttpGet]
        [Route("api/kategoriilanliste/{kategoriId}")]
        public List<KayitModel> KategoriIlanListe(string kategoriId)
        {
            List<KayitModel> liste = db.Kayit.Where(s => s.kayitKategoriId == kategoriId).Select(x => new KayitModel()
            {
                kayitId = x.kayitId,
                kayitKategoriId = x.kayitKategoriId,
                kayitIlanId = x.kayitIlanId,
            }).ToList();
            foreach (var kayit in liste)
            {
                kayit.ilanBilgi = IlanById(kayit.kayitIlanId);
                kayit.kategoriBilgi = KategoriById(kayit.kayitKategoriId);
            }
            return liste;
        }

        [HttpPost]
        [Route("api/kayitekle")]
        public SonucModel KayitEkle(KayitModel model)
        {
            if(db.Kayit.Count(s => s.kayitKategoriId == model.kayitKategoriId && s.kayitIlanId == model.kayitIlanId) > 0) {
                sonuc.islem = false;
                sonuc.mesaj = "İlan Kayıtlı!";
                return sonuc;
            }
            Kayit yeni = new Kayit();
            yeni.kayitId = Guid.NewGuid().ToString();
            yeni.kayitIlanId = model.kayitIlanId;
            yeni.kayitKategoriId = model.kayitKategoriId;
            db.Kayit.Add(yeni);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "İlan Eklendi!";
            return sonuc;
        }

        [HttpDelete]
        [Route("api/kayitsil/{kayitId}")]
        public SonucModel KayitSil(string kayitId)
        {
            Kayit kayit = db.Kayit.Where(s => s.kayitId == kayitId).SingleOrDefault();
            if (kayit == null)
            {
                sonuc.islem = false;
                sonuc.mesaj = "Kayıt Bulunamadı!";
                return sonuc;
            }
            db.Kayit.Remove(kayit);
            db.SaveChanges();
            sonuc.islem = true;
            sonuc.mesaj = "Kayıt Silindi";
            return sonuc;
        }
        #endregion
    }
}
