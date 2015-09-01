namespace Form115.Infrastructure.Search.Options
{
    #region UsingReg

    using System.Collections.Generic;
    using System.Linq;
    using DataLayer.Models;
    using Form115.Infrastructure.Search.Base;

    #endregion

    internal class SearchOptionPrixMin : SearchOption
    {
        private readonly int? _prixMin;

        public SearchOptionPrixMin(SearchBase sb, int? prixMin)
            : base(sb)
        {
            _prixMin = prixMin;
        }

        public override IEnumerable<Produits> GetResult()
        {
            return _prixMin.HasValue
                ? SearchBase.GetResult().Where(p => (p.PrixSolde != null ? p.PrixSolde : p.Prix) >= _prixMin)
                : SearchBase.GetResult();
        }
    }
}