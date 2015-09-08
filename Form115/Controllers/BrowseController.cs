using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using DataLayer.Models;
using Form115.Models;
using Form115.Infrastructure.Search;
using Form115.Infrastructure.Search.Base;
using Form115.Infrastructure.Search.Options;

namespace Form115.Controllers
{
    public class BrowseController : Controller
    {
        private Form115Entities _db = new Form115Entities();

        // GET: Browse
        public ActionResult Index()
        {
            var bvm = new BrowseViewModel();

            ViewBag.BestHotels = SearchController.GetSearchResult(bvm).OrderByDescending(o => o.Hotel.NbReservations).Take(2).ToList();
                        
            return View(bvm);
        }

        public static Dictionary<int, string> GetListeContinents()
        {
            Form115Entities db = new Form115Entities();
            return db.Continents.Where( c => db.Hotels
                                                                .Select(h => h.Villes.Pays.Regions.idContinent)
                                                                .Contains(c.idContinent))
                                                .Select(c => new { Key = c.idContinent, Value = c.name })
                                                .ToDictionary(x => x.Key, x => x.Value);
        }
        
        public JsonResult GetJsonRegions(int id)
        {
            var result = _db.Continents
                            .Find(id)
                            .Regions
                            .Where(r => _db.Hotels.Select(h => h.Villes.Pays.idRegion).Contains(r.idRegion))
                            .Select(r => new { Id = r.idRegion, Nom = r.name })
                            .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }

        public JsonResult GetJsonPays(int id)
        {
            var result = _db.Regions
                            .Find(id)
                            .Pays
                            .Where(p => _db.Hotels.Select(h => h.Villes.CodeIso3).Contains(p.CodeIso3))
                            .Select(p => new {Id = p.CodeIso3, Nom =p.Name.Trim()})
                            .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }
        
        public JsonResult GetJsonVilles(string id)
        {
            var result = _db.Pays
                            .Find(id)
                            .Villes
                            .Where(v => _db.Hotels.Select(h => h.IdVille).Contains(v.idVille))
                            .Select(v => new { Id = v.idVille, Nom = v.name })
                            .ToList();
            return Json(result, JsonRequestBehavior.AllowGet);
        }


        public JsonResult GetJsonBestHotels(int continent, int region, string pays, int ville)
        {
            var bvm = new BrowseViewModel
                {
                    Continent = continent,
                    Region = region,
                    Pays = pays,
                    Ville = ville
                };

           
            // TODO change to SearchResutPartialViewItem serializé ??
            return Json(SearchController.GetSearchResult(bvm)
                                        .OrderByDescending(o => o.Hotel.NbReservations)
                                        .Select(o => new
                                                     {
                                                         hotel =  new {
                                                                           nom = o.Hotel.Nom,
                                                                           ville = o.Hotel.Villes.name.Trim(),
                                                                           categorie = o.Hotel.Categorie.Value,
                                                                           photo = o.Hotel.Photo,
                                                                           id = o.Hotel.IdHotel
                                                                      },
                                                         produits = o.Produits
                                                                      .Select(p => new {
                                                                                            dateDepart = p.DateDepart.ToString("dd/MM/yyyy"),
                                                                                            prix = p.Promotion == 0 ? p.Prix : p.PrixSolde,
                                                                                            duree = p.Sejours.Duree
                                                                                        })
                                                                      .OrderBy(op => op.prix)
                                                     }
                                               )
                                        .Take(2).ToList(), JsonRequestBehavior.AllowGet);

        }

        public JsonResult GetJsonHotels(int continent, int region, string pays, int ville)
        {
            var bvm = new BrowseViewModel
            {
                Continent = continent,
                Region = region,
                Pays = pays,
                Ville = ville
            };


            // TODO change to SearchResutPartialViewItem serializé ??
            return Json(SearchController.GetSearchResult(bvm)
                                        .OrderByDescending(o => o.Hotel.NbReservations)
                                        .Select(o => new
                                        {
                                            hotel = new
                                            {
                                                nom = o.Hotel.Nom,
                                                ville = o.Hotel.Villes.name.Trim(),
                                                categorie = o.Hotel.Categorie.Value,
                                                photo = o.Hotel.Photo,
                                                id = o.Hotel.IdHotel
                                            },
                                            produits = o.Produits
                                                         .Select(p => new
                                                         {
                                                             dateDepart = p.DateDepart.ToString("dd/MM/yyyy"),
                                                             prix = p.Promotion == 0 ? p.Prix : p.PrixSolde,
                                                             duree = p.Sejours.Duree
                                                         })
                                                         .OrderBy(op => op.prix)
                                        }
                                               )
                                        .ToList(), JsonRequestBehavior.AllowGet);

        }

    }


}