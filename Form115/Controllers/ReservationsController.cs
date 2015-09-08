using DataLayer.Models;
using Form115.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;

namespace Form115.Controllers
{
    public class ReservationsController : Controller
    {
       public readonly Form115Entities db = new Form115Entities();

        // GET: Reservations/IdProduit/?quantite=NbPers
        public ActionResult Reserver(int id, int quantite)
        {

            var verif = db.Produits.Where(p=>p.IdProduit==id).First();
            var qteRes = verif.Reservations.Select(p => p.Quantity).Sum();

            if ((verif.NbPlaces-qteRes) - quantite >= 0)
            {
               
                var result=new Tuple<int,Produits>(quantite,verif);
                return View("Reserver",result);
            }
            else
            {
                return View("ReserverImpossible");
            }
        }

        [HttpPost]
        [Authorize]
        public ActionResult ConfirmerReservation(ReservationViewModel rvm)
        {
            //récupérer l'identifiant utilisateur de la personne connectée
            
            var user=db.Utilisateurs.Where(i=> i.IdAspNetUsers==rvm.IdUtilisateur).Select(x=>x.IdUtilisateur).FirstOrDefault();

            var ajout = new Reservations{
                IdProduit=rvm.IdProduit,
                Quantity=rvm.Quantity,
                IdUtilisateur=user,
                DateReservation=DateTime.Now
               
            };
            db.Reservations.Add(ajout);
                        

            db.SaveChanges();

            return View("Confirmation", rvm);
        }
    }
}