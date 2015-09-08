using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;


namespace Form115.Areas.Admin.Controllers
{
    public class HomeController : Controller
    {
        // GET: Admin/Home
        public ActionResult Index()
        {
            return View();
        }

        public JsonResult getStatsJson() {
            var db = new Form115Entities() ;
            var result = db.HotelTracking
                            .Where(pt => pt.DateHT >= new DateTime(2015, 8, 1) && pt.DateHT < new DateTime(2015, 9, 1))
                            .Join(db.Hotels,pt => pt.IdHotel,p => p.IdHotel,(pt, p) => new { pt.DateHT, p.Villes.Pays })
                            .GroupBy(x => x.Pays)
                            .Select(g => new { Nom = g.Key.Name.Trim(), NbVis = g.Count() })
                            .OrderBy(x => x.Nom) ;
                            //.ThenBy(x => x.Nom)
                            //.Take(10)
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}