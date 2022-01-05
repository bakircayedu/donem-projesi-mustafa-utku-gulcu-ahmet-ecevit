using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ProjeMVCV1._2.Models.Entity;

namespace ProjeMVCV1._2.Controllers
{
    public class HesapController : Controller
    {

        // GET: Hesap
        public ActionResult Index(long ID)
        {
            using(HusteDbEntities db = new HusteDbEntities())
            {
                var vergiNo = ID;

                var hesap = db.TblHesap.Find(vergiNo);

                MultipleClass2 mp = new MultipleClass2
                {
                    hesapDetaylari = hesap
                };

                return View(mp);
            }
        }

        [HttpPost]
        public ActionResult Index(MultipleClass2 mp)
        {
           using(HusteDbEntities db = new HusteDbEntities())
            {
                var hesap = db.TblHesap.Find(mp.hesapDetaylari.VergiNo);
                hesap.Unvan = mp.hesapDetaylari.Unvan;
                hesap.VergiDairesi = mp.hesapDetaylari.VergiDairesi;
                hesap.Adres = mp.hesapDetaylari.Adres;
                hesap.Sifre = mp.hesapDetaylari.Sifre;
                db.SaveChanges();
            }
            return View("~/Views/Menu/Index.cshtml");
        }
    }
}