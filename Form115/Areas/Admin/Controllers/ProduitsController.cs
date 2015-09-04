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
    public class ProduitsController : Controller
    {
        private Form115Entities db = new Form115Entities();

        // GET: Admin/Produits
        public ActionResult Index()
        {
            var produits = db.Produits.OrderBy(p=>p.IdProduit).Include(p => p.Sejours);
            return View(produits.ToList());
        }

        // GET: Admin/Produits/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produits produits = db.Produits.Find(id);
            if (produits == null)
            {
                return HttpNotFound();
            }
            return View(produits);
        }

        // GET: Admin/Produits/Create
        public ActionResult Create()
        {
            ViewBag.IdSejour = new SelectList(db.Sejours, "IdSejour", "IdSejour");
            return View();
        }

        public ActionResult MiseEnVenteSejour(int id)
        {
            ViewBag.IdSejour = new SelectList(db.Hotels, "IdSejour", "IdSejour");
         
            
            return View("Create");
        }

        // POST: Admin/Produits/Create
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "IdProduit,IdSejour,NbPlaces,DateDepart,Description,Prix")] Produits produits)
        {

            if (ModelState.IsValid)
            {
                db.Produits.Add(produits);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.IdSejour = new SelectList(db.Sejours, "IdSejour", "IdSejour", produits.IdSejour);
            return View(produits);
        }

        // GET: Admin/Produits/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produits produits = db.Produits.Find(id);
            if (produits == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdSejour = new SelectList(db.Sejours, "IdSejour", "IdSejour", produits.IdSejour);
            return View(produits);
        }

        // POST: Admin/Produits/Edit/5
        // Afin de déjouer les attaques par sur-validation, activez les propriétés spécifiques que vous voulez lier. Pour 
        // plus de détails, voir  http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "IdProduit,IdSejour,NbPlaces,DateDepart,Description,Prix")] Produits produits)
        {
            if (ModelState.IsValid)
            {
                db.Entry(produits).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdSejour = new SelectList(db.Sejours, "IdSejour", "IdSejour", produits.IdSejour);
            return View(produits);
        }

        // GET: Admin/Produits/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Produits produits = db.Produits.Find(id);
            if (produits == null)
            {
                return HttpNotFound();
            }
            return View(produits);
        }

        // POST: Admin/Produits/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Produits produits = db.Produits.Find(id);
            db.Produits.Remove(produits);
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

        public JsonResult GetJSONHotel(int id)
        {
            var db = new Form115Entities();
            var result = db.Sejours
                .Where(d => d.IdSejour == id)
                .Select(d => new { Hotel = d.Hotels.IdHotel, NomHotel = d.Hotels.Nom, Ville = d.Hotels.Villes.name, Pays = d.Hotels.Villes.Pays.Name }).OrderBy(x => x.Ville).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        public JsonResult GetJSONSejour()
        {
            var db = new Form115Entities();
            var result = db.Produits
                 .Select(d => new { Sejour = d.IdSejour, NomHotel = d.Sejours.Hotels.Nom, Ville = d.Sejours.Hotels.Villes.name, Pays = d.Sejours.Hotels.Villes.Pays.Name }).ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
    }
}
