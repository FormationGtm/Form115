namespace Form115.Infrastructure.Search.Options
{
    #region UsingReg

    using System.Collections.Generic;
    using System.Linq;
    using DataLayer.Models;
    using Form115.Infrastructure.Search.Base;

    #endregion

    internal class SearchOptionPrixMax : SearchOption
    {
        private readonly int? _prixMax;

        public SearchOptionPrixMax(SearchBase sb, int? prixMax)
            : base(sb)
        {
            _prixMax = prixMax;
        }

        public override IEnumerable<Produits> GetResult()
        {
            return  _prixMax.HasValue
                ? SearchBase.GetResult().Where(p => (p.PrixSolde != null ? p.PrixSolde : p.Prix) <= _prixMax)                                          
                : SearchBase.GetResult();
        }
    }
}