using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeMVCV1._2.Models.Entity;

namespace ProjeMVCV1._2.Controllers
{
    public class MenuController : Controller
    {
        HusteDbEntities db = new HusteDbEntities();

        // GET: Menu
        public ActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public ActionResult KullaniciGiris()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KullaniciGiris(TblHesap h1)
        {
            TempData["hesapVergiNo"] = h1.VergiNo;

            bool bilgiDogru = false;

            var vergiNolari = from h in db.TblHesap
                              select h;

            var sifreler = from h in db.TblHesap
                              select h.Sifre;
                        
            
            foreach (var item in vergiNolari.ToList())
            {

                if (h1.VergiNo == Convert.ToInt64(item.VergiNo))
                {
                    
                    if (h1.Sifre == item.Sifre)
                    {
                        bilgiDogru = true;
                    }

                }

            }

            /*foreach (var item in sifreler.ToList())
            {
                if (h1.Sifre == item)
                {
                    sifreDogru = true;
                }
            }*/

            if (bilgiDogru)
            {
                return View("Index");
            }

            else
            {

                if (h1.VergiNo == 0 || h1.Sifre == null)
                {
                    ViewBag.Mesaj = "Lütfen bir değer giriniz";
                }

                else
                {
                    ViewBag.Mesaj = "Vergi no veya şifre yanlış lütfen tekrar deneyiniz";
                }

                return View();
            }
        }

        [HttpGet]
        public ActionResult KayitEkrani()
        {
            return View();
        }

        [HttpPost]
        public ActionResult KayitEkrani(TblHesap hesap)
        {
            if (hesap.VergiNo == 0 || hesap.Unvan == null || hesap.Adres == null || hesap.VergiDairesi == null || hesap.Sifre == null)
            {
                ViewBag.Mesaj = "Lütfen bir değer giriniz";
                return View();
            }

            else
            {
                db.TblHesap.Add(hesap);
                db.SaveChanges();
                return View("KullaniciGiris");
            }
        }

        public ActionResult GirisEkrani()
        {
            return View();
        }
    }
}