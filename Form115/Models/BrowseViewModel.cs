using Form115.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Form115.Models
{
    public class BrowseViewModel
    {
        // Sélectiions à récupérer de la vue
        public int Continent { get; set; }
        public int Region { get; set; }
        public string Pays { get; set; }
        public int Ville { get; set; }

        // Liste des continents à passer à la vue
        public Dictionary<int, string> ListeContinents = BrowseController.GetListeContinents();
    }
}