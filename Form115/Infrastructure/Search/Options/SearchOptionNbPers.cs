namespace BestCars.Infrastructure.SearchAnnonces.Options
{
    #region UsingReg

    using System.Collections.Generic;
    using System.Linq;
    using DataLayer.Models;
    using Form115.Infrastructure.Search.Base;

    #endregion

    internal class SearchOptionNbPers : SearchOption {
        private readonly int? _nbPersonnes;

        public SearchOptionNbPers(SearchBase sb, int? nbp)
            : base(sb) {
            _nbPersonnes = nbp;
        }

        public override IEnumerable<Produits> GetResult()
        {
            var db = new Form115Entities(); 
            return _nbPersonnes.HasValue
                ? SearchBase.GetResult()
                             .Where(p => ((p.NbPlaces - (p.Reservations.Count() != 0 ? p.Reservations.Sum(r => r.Quantity) : 0)) >= _nbPersonnes))                                            
                : SearchBase.GetResult();
        }
    }
}