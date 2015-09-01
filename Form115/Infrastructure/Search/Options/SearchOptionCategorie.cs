namespace Form115.Infrastructure.Search.Options
{
    #region UsingReg

    using System.Collections.Generic;
    using System.Linq;
    using DataLayer.Models;
    using Form115.Infrastructure.Search.Base;
    using System;

    #endregion

    internal class SearchOptionCategorie : SearchOption
    {
        private readonly int[] _categorie;

        public SearchOptionCategorie(SearchBase sb, int[] categorie)
            : base(sb)
        {
            _categorie = categorie;
        }

        public override IEnumerable<Produits> GetResult()
        {
            return _categorie != null ?
                        SearchBase.GetResult()
                                  .Where(p => _categorie.Contains(p.Sejours.Hotels.Categorie.Value))
                        : SearchBase.GetResult();
        }
    }
}