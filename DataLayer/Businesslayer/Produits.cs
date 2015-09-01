using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
// HACK classe ajoutée
namespace DataLayer.Models
{

    public partial class Produits
    {
        private Form115Entities _db = new Form115Entities();

        // Fausse bonne idée : non reconnu par LinqForEntities
        //#region propriétés simple
        //// Propriétés permettant de parcourir la base lors des relations(Produits)n -> 1 , sans s
        //// pour se simplifier la vie lors des requêtes Linq
        //// getter uniquement

        //public virtual Sejours Sejour { get { return Sejours; } }

        //public virtual Hotels Hotel { get { return Sejours.Hotels; } }

        //public virtual Villes Ville { get { return Sejours.Hotels.Villes; } }

        //public virtual Pays Pays { get { return Sejours.Hotels.Villes.Pays; } }

        //public virtual Regions Region { get { return Sejours.Hotels.Villes.Pays.Regions; } }

        //public virtual Continents Continent { get { return Sejours.Hotels.Villes.Pays.Regions.Continents; } }

        //#endregion


        #region Prix

        public virtual byte Promotion
        {
            get
            {
                return Sejours.Hotels.Promotions.Where(p => (p.DateDebut <= DateDepart) && (p.DateFin >= DateDepart))
                                       .Select(p => p.Valeur)
                                       .FirstOrDefault();
            }

        }

        public virtual decimal? PrixSolde { get { return (100 - Promotion )* Prix /100; } }
        
        #endregion

        // Rq : non iutilisable  dans LinqForEntities alors que c'est le seul endroit où il est nécessaire de tester l'existence d'au moins
        // une Réservations associée au Produits pour la jointure (opérateur ternaire inutile dans LinqForObjects)
        // public virtual int Disponibilite { get {return NbPlaces - (Reservations.Count() != 0 ? Reservations.Sum(r => r.Quantity) : 0);}}

    }

}
