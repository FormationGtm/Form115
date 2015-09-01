using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;

namespace Form115.Controllers
{
    public class PromotionsController : Controller
    {
        private readonly Form115Entities db = new Form115Entities();

        // GET: Promotions
        public ActionResult Index()
        {
            // TODO appliquer un filtre  sur les produits et groupé par Promotion
            var promotions = db.Promotions
                               .Where(r => r.Hotels.Sejours
                                                    .Where(s => s.Produits
                                                                 .Where(p => p.DateDepart <= r.DateFin &&
                                                                                p.DateDepart >= r.DateDebut &&
                                                                                p.DateDepart > DateTime.Now)
                                                                 .Any())
                                                    .Any())
                               .OrderByDescending(x => x.Valeur);
            return View(promotions.ToList());
        }

    }
}
