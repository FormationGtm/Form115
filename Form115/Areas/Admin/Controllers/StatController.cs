using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Form115.Areas.Admin.Controllers
{
    public class StatController : Controller
    {
        [ChildActionOnly]
        public PartialViewResult Statistiques()
        {
            var db = new Form115Entities();

            var listProduitVisites = db.HotelTracking
                    .GroupBy(h=>h.Hotels.Nom)
                        .Select(g => new { NomHotel = g.Key, NbVis = g.Count() })
                .OrderByDescending(x => x.NbVis)
                 .Take(10)
                 .AsEnumerable()
                 .Select(x => new Tuple<string, int>(x.NomHotel, x.NbVis))
                 .ToList();
           
            return PartialView("_AdminStats", listProduitVisites);
            
            
            
        }
    }
}