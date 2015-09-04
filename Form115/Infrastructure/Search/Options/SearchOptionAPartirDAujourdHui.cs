namespace Form115.Infrastructure.Search.Options
{
    #region UsingReg

    using System.Collections.Generic;
    using System.Linq;
    using DataLayer.Models;
    using Form115.Infrastructure.Search.Base;
    using System;

    #endregion

    internal class SearchOptionAPartirDAujourdHui : SearchOption
    {
        public SearchOptionAPartirDAujourdHui(SearchBase sb)
            : base(sb)
        {
        }

        public override IEnumerable<Produits> GetResult()
        {
            return SearchBase.GetResult()
                             .Where(p => p.DateDepart >= DateTime.Now);
        }
    }
}