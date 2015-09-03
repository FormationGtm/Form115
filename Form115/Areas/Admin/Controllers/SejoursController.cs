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
    public class SejoursController : Controller
    {
        private Form115Entities db = new Form115Entities();

        // GET: Admin/Sejours
        public ActionResult Index()
        {
            var sejours = db.Sejours.Include(s => s.Hotels);
            return View(sejours.ToList());
        }

        // GET: Admin/Sejours/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sejours sejours = db.Sejours.Find(id);
            if (sejours == null)
            {
                return HttpNotFound();
            }
            return View(sejours);
        }

        // GET: Admin/Sejours/Create
        public ActionResult Create()
        {
            ViewBag.IdHotel = new SelectList(db.Hotels, "IdHotel", "Nom");
            return View();
        }

        public ActionResult CreerSejourPourHotel(int id)
        {
            ViewBag.IdHotel = new SelectList(db.Hotels, "IdHotel","Nom");
            Sejours h = new Sejours
            {
                IdHotel = id
                
            };
            ViewBag.Hotel = id;
            return View("Create");
        }

        // POST: Admin/Sejours/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdSejour,IdHotel,Duree")] Sejours sejours)
        {
            var req = db.Sejours.Where(x => x.IdHotel == sejours.IdHotel && x.Duree == sejours.Duree).Any();
            if (ModelState.IsValid && req == false)
            {
                db.Sejours.Add(sejours);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            

            ViewBag.IdHotel = new SelectList(db.Hotels, "IdHotel", "Nom", sejours.IdHotel);
            return View(sejours);
        }

        // GET: Admin/Sejours/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sejours sejours = db.Sejours.Find(id);
            if (sejours == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdHotel = new SelectList(db.Hotels, "IdHotel", "Nom", sejours.IdHotel);
            return View(sejours);
        }

        // POST: Admin/Sejours/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdSejour,IdHotel,Duree")] Sejours sejours)
        {
            if (ModelState.IsValid)
            {
                db.Entry(sejours).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdHotel = new SelectList(db.Hotels, "IdHotel", "Nom", sejours.IdHotel);
            return View(sejours);
        }

        // GET: Admin/Sejours/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Sejours sejours = db.Sejours.Find(id);
            if (sejours == null)
            {
                return HttpNotFound();
            }
            return View(sejours);
        }

        // POST: Admin/Sejours/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Sejours sejours = db.Sejours.Find(id);
            db.Sejours.Remove(sejours);
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
