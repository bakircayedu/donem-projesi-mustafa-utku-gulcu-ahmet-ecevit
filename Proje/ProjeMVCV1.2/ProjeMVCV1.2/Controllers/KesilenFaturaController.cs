using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeMVCV1._2.Models.Entity;

namespace ProjeMVCV1._2.Controllers
{
    public class KesilenFaturaController : Controller
    {
        HusteDbEntities db = new HusteDbEntities();
        // GET: KesilenFatura
        public ActionResult Index(int ID)
        {

            /*var hesap = from i in db.TblHesap
                        from f in db.TblKesilenFatura
                        where i.VergiNo == f.VergiNo
                        select i;*/


            var hesapBilgi = from i in db.TblHesap
                             from f in db.TblKesilenFatura
                             from fi in db.TblKesilenFaturaIcerik
                             where f.KesilenFaturaID == ID
                             where i.VergiNo == f.VergiNo
                             where f.KesilenFaturaID == fi.GidenFaturaNo
                             select i;

            var kesilenFaturaBilgi = from i in db.TblHesap
                             from f in db.TblKesilenFatura
                             from fi in db.TblKesilenFaturaIcerik
                             where f.KesilenFaturaID ==ID
                             where i.VergiNo == f.VergiNo
                             where f.KesilenFaturaID == fi.GidenFaturaNo
                             select f;


            var kesilenFaturaIcerikBilgi = from i in db.TblHesap
                                           from f in db.TblKesilenFatura
                                           from fi in db.TblKesilenFaturaIcerik
                                           where f.KesilenFaturaID==ID
                                           where i.VergiNo == f.VergiNo
                                           where f.KesilenFaturaID == fi.GidenFaturaNo
                                           select fi;


            MultipleClass2 mp = new MultipleClass2
            {
                hesapDetaylari = hesapBilgi.FirstOrDefault(),
                kesilenFaturaDetaylari = kesilenFaturaBilgi.FirstOrDefault(),
                KesilenFaturaIcerikDetaylari = kesilenFaturaIcerikBilgi.ToList()
            };


            List<decimal?> kdvTutarList = new List<decimal?>();
            List<decimal?> malHizmetTutarList = new List<decimal?>();
            decimal? toplamMalHizmetTutar = 0;

            foreach (var item in mp.KesilenFaturaIcerikDetaylari)
            {

                if (item.GidenFaturaUrunMiktari == null || item.GidenFaturaBirimFiyati == null || item.GidenFaturaKDVOrani == null)
                {
                    var kdvtutar = 0;
                    var malHizmetTutar = 0;
                    toplamMalHizmetTutar += malHizmetTutar;
                    kdvTutarList.Add(kdvtutar);
                    malHizmetTutarList.Add(malHizmetTutar);
                }

                else
                {
                    var kdvtutar = item.GidenFaturaBirimFiyati * item.GidenFaturaUrunMiktari * item.GidenFaturaKDVOrani / 100;
                    var malHizmetTutar = (item.GidenFaturaBirimFiyati * item.GidenFaturaUrunMiktari) + kdvtutar;
                    toplamMalHizmetTutar += malHizmetTutar;

                    kdvTutarList.Add(kdvtutar);
                    malHizmetTutarList.Add(malHizmetTutar);
                }

                
            }
            

            ViewBag.KDVTutari = kdvTutarList;
            ViewBag.malHizmetTutar = malHizmetTutarList;
            
            ViewBag.toplamMalHizmetTutar = Decimal.Round(Convert.ToDecimal(toplamMalHizmetTutar), 2);
            
            decimal toplamKDV = Convert.ToDecimal(toplamMalHizmetTutar * 0.18M);
            ViewBag.toplamKDV = Decimal.Round(toplamKDV, 2);

            decimal odenecekTutar = Convert.ToDecimal(toplamMalHizmetTutar + toplamKDV);
            ViewBag.odenecekTutar = Decimal.Round(odenecekTutar, 2);
            //ViewBag.odenecekTutar = Convert.ToDouble(odenecekTutar);

            ViewBag.odenecekTutarYazi = sayitoyazi(odenecekTutar);
            return View(mp);
        }

        public ActionResult GirisFaturasiGoster(int ID)
        {
            var hesapBilgi = from i in db.TblHesap
                             from f in db.TblBizeKesilenFatura
                             from fi in db.TblBizeKesilenFaturaIcerik
                             where f.BizeKesilenFaturaID== ID
                             where i.VergiNo == f.VergiNo
                             where f.BizeKesilenFaturaID == fi.GelenFaturaNo
                             select i;

            var bizeKesilenFaturaBilgi = from i in db.TblHesap
                                     from f in db.TblBizeKesilenFatura
                                     from fi in db.TblBizeKesilenFaturaIcerik
                                     where f.BizeKesilenFaturaID == ID
                                     where i.VergiNo == f.VergiNo
                                     where f.BizeKesilenFaturaID == fi.GelenFaturaNo
                                     select f;


            var bizeKesilenFaturaIcerikBilgi = from i in db.TblHesap
                                           from f in db.TblBizeKesilenFatura
                                           from fi in db.TblBizeKesilenFaturaIcerik
                                           where f.BizeKesilenFaturaID == ID
                                           where i.VergiNo == f.VergiNo
                                           where f.BizeKesilenFaturaID == fi.GelenFaturaNo
                                           select fi;


            MultipleClass2 mp = new MultipleClass2
            {
                hesapDetaylari = hesapBilgi.FirstOrDefault(),
                bizeKesilenFaturaDetaylari = bizeKesilenFaturaBilgi.FirstOrDefault(),
                bizeKesilenFaturaIcerikDetaylari = bizeKesilenFaturaIcerikBilgi.ToList()
            };


            List<decimal?> kdvTutarList = new List<decimal?>();
            List<decimal?> malHizmetTutarList = new List<decimal?>();
            decimal? toplamMalHizmetTutar = 0;

            foreach (var item in mp.bizeKesilenFaturaIcerikDetaylari)
            {
                if (item.GelenFaturaUrunMiktar == null || item.GelenFaturaBirimFiyat == null || item.GelenFaturaKDVOrani == null)
                {
                    var kdvtutar = 0;
                    var malHizmetTutar = 0;
                    toplamMalHizmetTutar += malHizmetTutar;
                    kdvTutarList.Add(kdvtutar);
                    malHizmetTutarList.Add(malHizmetTutar);
                }

                else
                {
                    var kdvtutar = item.GelenFaturaBirimFiyat * item.GelenFaturaUrunMiktar * item.GelenFaturaKDVOrani / 100;
                    var malHizmetTutar = (item.GelenFaturaBirimFiyat * item.GelenFaturaUrunMiktar) + kdvtutar;
                    toplamMalHizmetTutar += malHizmetTutar;
                    kdvTutarList.Add(kdvtutar);
                    malHizmetTutarList.Add(malHizmetTutar);
                }
                
            }


            ViewBag.KDVTutari = kdvTutarList;
            ViewBag.malHizmetTutar = malHizmetTutarList;

            ViewBag.toplamMalHizmetTutar = Decimal.Round(Convert.ToDecimal(toplamMalHizmetTutar), 2);

            decimal toplamKDV = Convert.ToDecimal(toplamMalHizmetTutar * 0.18M);
            ViewBag.toplamKDV = Decimal.Round(toplamKDV, 2);

            decimal odenecekTutar = Convert.ToDecimal(toplamMalHizmetTutar + toplamKDV);
            ViewBag.odenecekTutar = Decimal.Round(odenecekTutar, 2);
            //ViewBag.odenecekTutar = Convert.ToDouble(odenecekTutar);

            ViewBag.odenecekTutarYazi = sayitoyazi(odenecekTutar);
            return View(mp);
        }


        /*public ActionResult FaturaListele()
        {
            return View();
        }*/

        [HttpGet]
        public ActionResult FaturaOlustur()
        {
            ViewBag.ID = TempData["hesapVergiNo"];
            TempData.Keep("hesapVergiNo");
            return View();
        }


        [HttpPost]
        public ActionResult FaturaOlustur(MultipleClass2 mp)
        {
            TblBizeKesilenFatura bkf = new TblBizeKesilenFatura
            {
                GelenFaturaVergiNo = mp.bizeKesilenFaturaDetaylari.GelenFaturaVergiNo,
                GelenFaturaUnvan = mp.bizeKesilenFaturaDetaylari.GelenFaturaUnvan,
                VergiNo = Convert.ToInt64(TempData["hesapVergiNo"]),
                GelenFaturaVergiDairesi = mp.bizeKesilenFaturaDetaylari.GelenFaturaVergiDairesi,
                GelenFaturaAdres = mp.bizeKesilenFaturaDetaylari.GelenFaturaAdres,
                GelenFaturaTarih = mp.bizeKesilenFaturaDetaylari.GelenFaturaTarih

            };

            TempData.Keep("hesapVergiNo");

            db.TblBizeKesilenFatura.Add(bkf);

            int satirNo = 1;

            var maxID = db.TblBizeKesilenFaturaIcerik.Max(m => m.GelenFaturaID);

            foreach (var item in mp.bizeKesilenFaturaIcerikDetaylari)
            {
                item.GelenFaturaSatirNo = satirNo;
                item.GelenFaturaID = maxID + 1;
                db.TblBizeKesilenFaturaIcerik.Add(item);
                satirNo++;
                maxID++;
            }

            db.SaveChanges();

            return View("FaturalariListele");
        }

        [HttpGet]
        public ActionResult CikisFaturasiOlustur()
        {
            ViewBag.ID = TempData["hesapVergiNo"];
            TempData.Keep("hesapVergiNo");
            return View();
        }

        [HttpPost]
        public ActionResult CikisFaturasiOlustur(MultipleClass2 mp)
        {
            TblKesilenFatura kf = new TblKesilenFatura
            {
                GidenFaturaVergiNo = mp.kesilenFaturaDetaylari.GidenFaturaVergiNo,
                GidenFaturaUnvan = mp.kesilenFaturaDetaylari.GidenFaturaUnvan,
                VergiNo = Convert.ToInt64(TempData["hesapVergiNo"]),
                GidenFaturaVergiDairesi = mp.kesilenFaturaDetaylari.GidenFaturaVergiDairesi,
                GidenFaturaAdres = mp.kesilenFaturaDetaylari.GidenFaturaAdres,
                GidenFaturaTarih = mp.kesilenFaturaDetaylari.GidenFaturaTarih
            };

            TempData.Keep("hesapVergiNo");


            db.TblKesilenFatura.Add(kf);

            int satirNo = 1;

            var maxID = db.TblKesilenFaturaIcerik.Max(m => m.GidenFaturaID);

            
            foreach (var item in mp.KesilenFaturaIcerikDetaylari)
            {
                item.KesilanFaturaSatirNo = satirNo;
                item.GidenFaturaID= Convert.ToInt32(maxID) + 1;
                db.TblKesilenFaturaIcerik.Add(item);
                satirNo++;
                maxID++;
            }

            db.SaveChanges();

            return View("FaturalariListele");
        }

        public ActionResult FaturalariListele()
        {
            ViewBag.ID = TempData["hesapVergiNo"];
            TempData.Keep("hesapVergiNo");
            return View();
        }

        public ActionResult GirisFaturalariListele()
        {
            long vergiNo = Convert.ToInt64(TempData["hesapVergiNo"]);
            TempData.Keep("hesapVergiNo");

            var bkfBilgi = from bkf in db.TblBizeKesilenFatura
                           where bkf.VergiNo == vergiNo
                           select bkf;

            /*var bkfIcerikBilgi = from bkfIcerik in db.TblBizeKesilenFaturaIcerik
                                 select bkfIcerik;*/


            MultipleClass2 mp = new MultipleClass2
            {
                bizeKesilenFaturaDetaylariList = bkfBilgi.ToList()
                //bizeKesilenFaturaIcerikDetaylari = bkfIcerikBilgi.ToList()
            };

            return PartialView(mp);
        }

        public ActionResult CikisFaturalariListele()
        {
            ViewBag.ID = TempData["hesapVergiNo"];
            TempData.Keep("hesapVergiNo");

            long vergiNo = Convert.ToInt64(TempData["hesapVergiNo"]);
            TempData.Keep("hesapVergiNo");

            var kfBilgi = from kf in db.TblKesilenFatura
                          where kf.VergiNo == vergiNo
                           select kf;

            /*var kfIcerikBilgi = from kfIcerik in db.TblKesilenFaturaIcerik
                                 select kfIcerik;*/


            MultipleClass2 mp = new MultipleClass2
            {
                KesilenFaturaDetaylariList = kfBilgi.ToList()
                //KesilenFaturaIcerikDetaylari = kfIcerikBilgi.ToList()
            };

            return PartialView(mp);
        }

        public ActionResult Muhasebe()
        {
            ViewBag.ID = TempData["hesapVergiNo"];
            TempData.Keep("hesapVergiNo");

            long hesapVergiNo = Convert.ToInt64(TempData["hesapVergiNo"]);
            TempData.Keep("hesapVergiNo");


            var kesilenFaturaIcerikBilgi = from kf in db.TblKesilenFatura
                        from kfIcerik in db.TblKesilenFaturaIcerik
                        where kf.VergiNo == hesapVergiNo
                        where kf.KesilenFaturaID == kfIcerik.GidenFaturaNo
                        select kfIcerik;

            var bizeKesilenFaturaIcerikBilgi = from bkf in db.TblBizeKesilenFatura
                                           from bkfIcerik in db.TblBizeKesilenFaturaIcerik
                                           where bkf.VergiNo == hesapVergiNo
                                           where bkf.BizeKesilenFaturaID == bkfIcerik.GelenFaturaNo
                                           select bkfIcerik;

            
            //yeni eklendi
            var varlikBilgi = from v in db.TblVarliklar
                         where v.KullaniciVergiNo == hesapVergiNo
                         select v;

            MultipleClass2 mp = new MultipleClass2
            {
                KesilenFaturaIcerikDetaylari = kesilenFaturaIcerikBilgi.ToList(),
                bizeKesilenFaturaIcerikDetaylari = bizeKesilenFaturaIcerikBilgi.ToList()
            };

            decimal toplamGelir = 0;
            decimal toplamGider = 0;
            decimal toplamGelirKDV = 0;
            decimal toplamGiderKDV = 0;

            foreach (var item in mp.KesilenFaturaIcerikDetaylari)
            {
                toplamGelir += Convert.ToDecimal((item.GidenFaturaUrunMiktari * item.GidenFaturaBirimFiyati * item.GidenFaturaKDVOrani / 100) + (item.GidenFaturaUrunMiktari * item.GidenFaturaBirimFiyati));
                toplamGelirKDV += Convert.ToDecimal(item.GidenFaturaUrunMiktari * item.GidenFaturaBirimFiyati * item.GidenFaturaKDVOrani / 100);
            }

            foreach (var item in mp.bizeKesilenFaturaIcerikDetaylari)
            {
                toplamGider += Convert.ToDecimal((item.GelenFaturaUrunMiktar * item.GelenFaturaBirimFiyat * item.GelenFaturaKDVOrani / 100) + (item.GelenFaturaUrunMiktar * item.GelenFaturaBirimFiyat));
                toplamGiderKDV += Convert.ToDecimal(item.GelenFaturaUrunMiktar * item.GelenFaturaBirimFiyat * item.GelenFaturaKDVOrani / 100);
            }

            decimal toplamKDV = toplamGelirKDV + toplamGiderKDV;
            decimal gelirVergisi = (toplamGelir - toplamGider) / 5;

            //yeni eklendi
            TblVarliklar varlik = new TblVarliklar();
            TblVarliklar varlik_ = new TblVarliklar();

            varlik_ = varlikBilgi.FirstOrDefault();

            try
            {
                int varlikID = Convert.ToInt32(varlik_.VarlikID);
                var varlik2 = db.TblVarliklar.Find(varlikID);
                varlik2.Girdiler = toplamGelir;
                varlik2.Ciktilar = toplamGider;
                db.SaveChanges();
            }
            catch (Exception)
            {
                //var bulunanVarlik = varlik;
                
                varlik.KullaniciVergiNo = hesapVergiNo;
                varlik.Girdiler = toplamGelir;
                varlik.Ciktilar = toplamGider;
                db.TblVarliklar.Add(varlik);
                db.SaveChanges();
            }

            ViewBag.toplamGelir = Decimal.Round(toplamGelir, 2);
            ViewBag.toplamGider = Decimal.Round(toplamGider, 2);
            ViewBag.toplamKDV = Decimal.Round(toplamKDV, 2);
            ViewBag.gelirVergisi = decimal.Round(gelirVergisi, 2);

            return View();
        }

        public ActionResult GirisFaturasiSil(int ID)
        {
            //List<int> IDler = new List<int>();

            var faturaID = from f in db.TblBizeKesilenFaturaIcerik
                        where f.GelenFaturaNo == ID
                        select f.GelenFaturaID;

            //IDler = faturaID.ToList();

            foreach (var item in faturaID)
            {
                var silinecekIcerik= db.TblBizeKesilenFaturaIcerik.Find(item);
                db.TblBizeKesilenFaturaIcerik.Remove(silinecekIcerik);
            }

            var silinecekFatura = db.TblBizeKesilenFatura.Find(ID);
            db.TblBizeKesilenFatura.Remove(silinecekFatura);
            db.SaveChanges();

            return View("FaturalariListele");
        }

        public ActionResult CikisFaturasiSil(int ID)
        {
            //List<int> IDler = new List<int>();

            var faturaID = from f in db.TblKesilenFaturaIcerik
                           where f.GidenFaturaNo == ID
                           select f.GidenFaturaID;

            //IDler = faturaID.ToList();

            foreach (var item in faturaID)
            {
                var silinecekIcerik = db.TblKesilenFaturaIcerik.Find(item);
                db.TblKesilenFaturaIcerik.Remove(silinecekIcerik);
            }

            var silinecekFatura = db.TblKesilenFatura.Find(ID);
            db.TblKesilenFatura.Remove(silinecekFatura);
            db.SaveChanges();

            return View("FaturalariListele");
        }


        private string sayitoyazi(decimal tutar)
        {
            string sTutar = tutar.ToString("F2").Replace('.', ',');
            string lira = sTutar.Substring(0, sTutar.IndexOf(','));
            string kurus = sTutar.Substring(sTutar.IndexOf(',') + 1, 2);
            string yazi = "";

            string[] birler = { "", "BİR", "İKİ", "ÜÇ", "DÖRT", "BEŞ", "ALTI", "YEDİ", "SEKİZ", "DOKUZ" };
            string[] onlar = { "", "ON", "YİRMİ", "OTUZ", "KIRK", "ELLİ", "ALTMIŞ", "YETMİŞ", "SEKSEN", "DOKSAN" };
            string[] binler = { "KATRİLYON", "TRİLYON", "MİLYAR", "MİLYON", "BİN", "" };

            int grupSayisi = 6;


            lira = lira.PadLeft(grupSayisi * 3, '0');

            string grupDegeri;

            for (int i = 0; i < grupSayisi * 3; i += 3)
            {
                grupDegeri = "";

                if (lira.Substring(i, 1) != "0")
                    grupDegeri += birler[Convert.ToInt32(lira.Substring(i, 1))] + "YÜZ";

                if (grupDegeri == "BİRYÜZ")
                    grupDegeri = "YÜZ";

                grupDegeri += onlar[Convert.ToInt32(lira.Substring(i + 1, 1))];

                grupDegeri += birler[Convert.ToInt32(lira.Substring(i + 2, 1))];

                if (grupDegeri != "")
                    grupDegeri += binler[i / 3];

                if (grupDegeri == "BİRBİN")
                    grupDegeri = "BİN";

                yazi += grupDegeri;
            }

            if (yazi != "")
                yazi += " TL ";

            int yaziUzunlugu = yazi.Length;

            if (kurus.Substring(0, 1) != "0")
                yazi += onlar[Convert.ToInt32(kurus.Substring(0, 1))];

            if (kurus.Substring(1, 1) != "0")
                yazi += birler[Convert.ToInt32(kurus.Substring(1, 1))];

            if (yazi.Length > yaziUzunlugu)
                yazi += " Kr.";
            else
                yazi += "SIFIR Kr.";

            return yazi;
        }

    }
}