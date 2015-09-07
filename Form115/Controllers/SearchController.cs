using BestCars.Infrastructure.SearchAnnonces;
using DataLayer.Models;
using Form115.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Form115.Infrastructure.Search;
using Form115.Infrastructure.Search.Base;
using Form115.Infrastructure.Search.Options;
using BestCars.Infrastructure.SearchAnnonces.Options;

namespace Form115.Controllers
{
    public class SearchController : Controller
    {
        private readonly Form115Entities _db = new Form115Entities();
        // GET: Browse
        public ActionResult Index()
        {
            var svm = new SearchViewModel();

            // TODO classe Categorie qui renverra la liste des catégories (méthode statique)
            svm.ListeCategories = _db.Categories.Select(c => new { Key = c.IdCategorie, Value = c.Description }).ToDictionary(x => x.Key, x => x.Value);
            svm.DisponibiliteMax = _db.Produits.Select(p => p.NbPlaces).Max();
            svm.DisponibiliteMax = 20;
            svm.DureeMini = _db.Produits.Select(p => p.Sejours.Duree).Min();
            svm.DureeMaxi = _db.Produits.Select(p => p.Sejours.Duree).Max();
            svm.PrixMini = (int)Math.Floor((double)_db.Produits.Select(p => p.Prix).Min());
            svm.PrixMaxi = (int)Math.Ceiling((double)_db.Produits.Select(p => p.Prix).Max());
            svm.DateDepart = DateTime.Now;

            return View(svm);
        }

        [HttpPost]
        public ActionResult Index(SearchViewModel svm)
        {
            // TODO classe Categorie qui renverra la liste des catégories (méthode statique)
            svm.ListeCategories = _db.Categories.Select(c => new { Key = c.IdCategorie, Value = c.Description }).ToDictionary(x => x.Key, x => x.Value);
            svm.DisponibiliteMax = _db.Produits.Select(p => p.NbPlaces).Max();

            return View(svm);
        }



        public ActionResult Result(int id)
        {
            var svm = new SearchViewModel { Ville = id, DateIndifferente = new int[] {1} };
            var result = GetSearchResult(svm);

            return View(result);
        }

        [HttpPost]
        public ActionResult Result(SearchViewModel svm)
        {
            //_listBreadCrumb.Add(new BreadCrumbItem { Texte = "Résultat", ControllerName = "Search", ActionName = "Result" });
            //ViewBag.ListeCrumbItem = _listBreadCrumb;

            // var pageSize = 20;
            var result = GetSearchResult(svm);

            //var rvm = new SearchResultViewModel
            //{
            //    ListeResultat = result.Take(pageSize).ToList(),
            //    PageSize = pageSize,
            //    ItemsQty = result.Count(),
            //    PagesQty = (result.Count() % pageSize) == 0 ? result.Count() / pageSize : result.Count() / pageSize + 1,
            //    CurrentPage = 1,
            //    XmlSearchViewModel = svm.SerializeSearchViewModel()
            //};
            //return View(rvm);// l.Select(v => new {v.IdVoiture, v.TypeVendeur, v.NumDep, v.Prix}
            ViewBag.ListResults = result;
            return View(svm);
        }

        public static List<SearchResutPartialViewItem> GetSearchResult(BrowseViewModel bvm)
        {
            SearchBase s = new Search();
            s = new SearchOptionDestination(s, bvm.Continent, bvm.Region, bvm.Pays, bvm.Ville);
            s = new SearchOptionAPartirDAujourdHui(s);
            s = new SearchOptionNbPers(s, 1);
            return OrderingGroupResult(s);
        }

        public static List<SearchResutPartialViewItem> GetSearchResult(SearchViewModel svm)
        {
            // Search et SearchOption héritent de SearchBase
            SearchBase s = new Search();
            s = new SearchOptionDestination(s, svm.Continent, svm.Region, svm.Pays, svm.Ville);
            if (svm.DateIndifferente == null)
            {
                s = new SearchOptionDateDepart(s, svm.DateDepart);
            }
            s = new SearchOptionAPartirDAujourdHui(s);
            // TODO Attention ici il peut n'y avoir qu'une seule renseignée
            s = new SearchOptionDuree(s, svm.DureeMini, svm.DureeMaxi);
            s = new SearchOptionNbPers(s, svm.NbPers);
            s = new SearchOptionCategorie(s, svm.Categorie);
            s = new SearchOptionPrixMax(s, svm.PrixMaxi);
            s = new SearchOptionPrixMin(s, svm.PrixMini);

            // Intégration de DateDepart > DateTime.Now ici car on n'est pas intéressé par un produit périmé
            return OrderingGroupResult(s);
        }

        internal static List<SearchResutPartialViewItem> OrderingGroupResult(SearchBase s)
        {
            Form115Entities db = new Form115Entities();

            return  s.GetResult()
                    .GroupBy(p => p.Sejours.Hotels.IdHotel,
                             p => p,
                             (key, g) => new SearchResutPartialViewItem
                             {
                                 Hotel = db.Hotels.Where(h => h.IdHotel == key).FirstOrDefault(),
                                 Produits = g.ToList()
                             })
                    .ToList();
        }

        public PartialViewResult PartialSearchResult(SearchResutPartialViewItem srpvi)
        {
            return PartialView("_SearchResutPartialView", srpvi);
        }

    
    }
    
}