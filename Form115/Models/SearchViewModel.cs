using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web;

namespace Form115.Models
{
    public class SearchViewModel : BrowseViewModel
    {
        // Informations retournées par le formulaire
        // TODO validation de formulaire
        public DateTime DateDepart { get; set; }
        public int[] DateIndifferente { get; set; }
        public int? DateMarge { get; set; }
        public byte? DureeMini { get; set; }
        public byte? DureeMaxi { get; set; }
        public int? PrixMini { get; set; }
        public int? PrixMaxi { get; set; }
        public int[] Categorie { get; set; }
        public int? NbPers { get; set; }

        // Informations de liste à envoyer à la BDD
        public Dictionary<byte, string> ListeCategories { get; set; }
        public int DisponibiliteMax { get; set; }

        public SearchViewModel() { }
        public SearchViewModel(BrowseViewModel parent)
        {
            foreach (PropertyInfo prop in parent.GetType().GetProperties())
            {
                GetType().GetProperty(prop.Name).SetValue(this, prop.GetValue(parent, null), null);
            }               
        }
    }
}