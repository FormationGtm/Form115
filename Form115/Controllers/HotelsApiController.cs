using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using DataLayer.Models;
using Form115.Models;

namespace Form115.Controllers
{
    public class HotelsApiController : ApiController
    {
        private Form115Entities db = new Form115Entities();

        // GET: api/HotelsApi/5
        [ResponseType(typeof(Hotels))]
        public IHttpActionResult GetHotels()
        {

            var db = new Form115Entities() ;
            var meilleurePromo = db.Promotions.OrderByDescending(p=>p.Valeur).FirstOrDefault() ;
            if (meilleurePromo == null)
            {
                return NotFound();
            }
            
            Hotels hotel = meilleurePromo.Hotels ;
            var retour = new HotelsApiModel {
                Titre = hotel.Nom,
                Localisation = hotel.Villes.name,
                Prix = db.Produits.Where(p=>p.Sejours.IdHotel==hotel.IdHotel).Where(p=>p.DateDepart>=meilleurePromo.DateDebut).First().Prix.ToString(),
                Description = hotel.Description,
                Image = "http://form115.dlucazeau.fr/Admin/Uploads/"+hotel.Photo,
                URL = "http://form115.dlucazeau.fr/Hotel/Details/"+hotel.IdHotel
            };
            //var retour2 = new HotelsApiModel
            //{
            //    Titre = hotel.Nom,
            //    Localisation = hotel.Villes.name,
            //    Prix = db.Produits.Where(p => p.Sejours.IdHotel == hotel.IdHotel).Where(p => p.DateDepart >= meilleurePromo.DateDebut).First().Prix.ToString(),
            //    Description = hotel.Description,
            //    Image = Url.Action("http://form115.dlucazeau.fr/Admin/Uploads/" + hotel.Photo,
            //    URL = "http://form115.dlucazeau.fr/Hotel/Details/" + hotel.IdHotel
            //};


            return Ok(retour);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool HotelsExists(int id)
        {
            return db.Hotels.Count(e => e.IdHotel == id) > 0;
        }
    }
}