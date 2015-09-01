namespace Form115.Infrastructure.Search.Options
{
    #region UsingReg

    using System.Collections.Generic;
    using System.Linq;
    using DataLayer.Models;
    using Form115.Infrastructure.Search.Base;
    using System;

    #endregion

    internal class SearchOptionDuree : SearchOption
    {
        private readonly byte? _duree;

        public SearchOptionDuree(SearchBase sb, byte? duree)
            : base(sb)
        {
            _duree = duree;
        }

        public override IEnumerable<Produits> GetResult()
        {
            return  _duree.HasValue
                ? SearchBase.GetResult()
                            .Where(p => Math.Abs((decimal)(p.Sejours.Duree - _duree)) <= 2)
                : SearchBase.GetResult();
        }
    }
}