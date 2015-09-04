using Form115.Infrastructure.Filters;
using DataLayer.Models;
using Form115.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace Form115.Controllers
{
    public class HotelController : Controller
    {
        protected ApplicationDbContext ApplicationDbContext { get; set; }
        protected UserManager<ApplicationUser> UserManager { get; set; }

        public HotelController()
        {
            this.ApplicationDbContext = new ApplicationDbContext();
            this.UserManager = new UserManager<ApplicationUser>(new UserStore<ApplicationUser>(this.ApplicationDbContext));
        }

        private Form115Entities _db = new Form115Entities();

        // GET: Hotel
        public ActionResult Index()
        {
            return View();
        }

        [HotelTrackerFilter]
        public ActionResult Details(int id)
        {
            //Form115Entities db = new Form115Entities();
            //Hotels hotel = db.Hotels.Where(h => h.IdHotel == id).First();
            HotelViewModel hvm = new HotelViewModel
            {
                IdHotel = id
            };
            return View(hvm);
        }

        [HotelTrackerFilter]
        public ActionResult DetailsPeriode(int id, string startDate, string endDate)
        {
            //Form115Entities db = new Form115Entities();
            //Hotels hotel = db.Hotels.Where(h => h.IdHotel == id).First();
            HotelViewModel hvm = new HotelViewModel
            {
                IdHotel = id,
                DateDebut = startDate,
                DateFin = endDate
            };
            return View("Details", hvm);
        }
        

        [HttpPost]
        public JsonResult listeProduits(HotelViewModel hvm)
        {
            var prods = _db.Produits.Where(p => p.Sejours.IdHotel == hvm.IdHotel)
                            .Where(p=>p.Sejours.Duree >= hvm.DureeMinSejour) ; 
            // TODO Attention aux filtres concurents pour le dateDebut
            if (hvm.DureeMaxSejour != null) {
                prods = prods.Where(p=>p.Sejours.Duree<=hvm.DureeMaxSejour) ;         
            }
            if (hvm._dateDepart!=null) {
                prods = prods.Where(p=>p.DateDepart >= hvm._dateDepart) ;
            }
            //if (hvm._dateDebut != null)
            //{
                prods = prods.Where(p => p.DateDepart >= hvm._dateDebut);
            //}
            ////if (hvm._dateFin != null)
            //{
                prods = prods.Where(p => p.DateDepart <= hvm._dateFin);
            //}

            // HACK AsEnumerable avant le select ? Sinon ATTENTION, le nb_restants ne sera
            // pas à jour pour les prouits n'ayant pas de réservation, nécessite opérateur ternaire poutr jointure externe
            var result = prods.AsEnumerable().Select(p => new {
                                date = p.DateDepart.ToString("dd/MM/yyyy"), 
                                duree = p.Sejours.Duree,
                                prix = p.Prix, 
                                promotions = p.Promotion,
                                prixSolde = p.PrixSolde,
                                nb_restants = p.NbPlaces - p.Reservations.Sum(r => r.Quantity)
                            });
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        // En cours
        //public MvcHtmlString AfficherCommentaires(HtmlHelper Html, List<Commentaires> commentaires, int? idCommentaire, int cpt)
        //{
        //    foreach (var com in commentaires.Where(c => c.IdCommentaire == idCommentaire))
        //    {
        //        Html.
        //        AfficherCommentaires(commentaires, com.IdCommentaire, cpt + 1);
        //    }
        //}

        [Authorize]
        [HttpPost, ValidateInput(false)]
        public ActionResult Comment(CommentViewModel cvm, int id)
        {
            // TODO debut : problème avec le GetUSerId ici
            // TODO tester mlodèle + règles métiers sur entrée utilisateurs
            // + vérifier enrigtrement BDD pour afficher éventuellement petit message
            var user = UserManager.FindById(User.Identity.GetUserId());
            var commentaire = new Commentaires
            {
                IdHotel = id,
                IdUtilisateur = _db.Utilisateurs.Where(u => u.IdAspNetUsers == user.Id)
                                                .Select(u => u.IdUtilisateur)
                                                .FirstOrDefault(),
                Titre = cvm.Titre,
                Commentaire = cvm.Commentaire,
                DateCommentaire = DateTime.Now,
                IdCommentaireReference = cvm.IdCommentaire
            };

            _db.Commentaires.Add(commentaire);
            _db.SaveChanges();

            return RedirectToAction("Details", new {id = id});
        }

        public PartialViewResult PartialComment(Hotels hotel)
        {
            return PartialView("_CommentView", new CommentViewModel { Hotel = hotel });
        }
    }
}