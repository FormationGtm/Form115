using DataLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Form115.Controllers
{
    public class ReservationsController : Controller
    {
       public readonly Form115Entities db = new Form115Entities();

        // GET: Reservations
        public ActionResult Reserver(int? id, int? quantite)
        {

            var verif = db.Produits.Where(p => p.IdSejour == 1003).First();

            if (verif.NbPlaces > quantite && verif.NbPlaces - quantite > 0)
            {
                
                return View("Reserver");
            }
            else
            {
                return View("ReserverImpossible");
            }
        }
    }
}