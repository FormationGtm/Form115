using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;

namespace Form115.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin")]
    public class PromotionsController : Controller
    {
        private Form115Entities db = new Form115Entities();

        // GET: Admin/Promotions
        public ActionResult Index()
        {
            var promotions = db.Promotions.Include(p => p.Hotels);
            return View(promotions.ToList());
        }

        // GET: Admin/Promotions/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotions promotions = db.Promotions.Find(id);
            if (promotions == null)
            {
                return HttpNotFound();
            }
            return View(promotions);
        }

        // GET: Admin/Promotions/Create
        public ActionResult Create()
        {
            ViewBag.IdHotel = new SelectList(db.Hotels, "IdHotel", "Nom");
            return View();
        }

        public ActionResult CreerPourHotel(int id)
        {
            ViewBag.IdHotel = new SelectList(db.Hotels, "IdHotel", "Nom");
            ViewBag.idHotelASelectionner = id;
            return View("Create");
        }

        // POST: Admin/Promotions/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdPromo,IdHotel,DateDebut,DateFin,Valeur")] Promotions promotions)
        {
            if (ModelState.IsValid)
            {
                Hotels hotels = db.Hotels.Find(promotions.IdHotel);
                try
                {
                    hotels.ajouterPromotion(db, promotions);
                }
                catch (Exception) 
                {
                    ViewBag.IdHotel = new SelectList(db.Hotels, "IdHotel", "Nom");
                    ViewBag.MessageErreur = "Impossible d'ajouter cette promotion car elle chevauche une promotion existante.";
                    return View(promotions);
                }
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdHotel = new SelectList(db.Hotels, "IdHotel", "Nom", promotions.IdHotel);
            return View(promotions);
        }

        // GET: Admin/Promotions/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotions promotions = db.Promotions.Find(id);
            if (promotions == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdHotel = new SelectList(db.Hotels, "IdHotel", "Nom", promotions.IdHotel);
            return View(promotions);
        }

        // POST: Admin/Promotions/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdPromo,IdHotel,DateDebut,DateFin,Valeur")] Promotions promotions)
        {
            if (ModelState.IsValid)
            {
                db.Entry(promotions).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdHotel = new SelectList(db.Hotels, "IdHotel", "Nom", promotions.IdHotel);
            return View(promotions);
        }

        // GET: Admin/Promotions/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Promotions promotions = db.Promotions.Find(id);
            if (promotions == null)
            {
                return HttpNotFound();
            }
            return View(promotions);
        }

        // POST: Admin/Promotions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Promotions promotions = db.Promotions.Find(id);
            db.Promotions.Remove(promotions);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
