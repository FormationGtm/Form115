namespace Form115.Infrastructure.Search.Options
{
    #region UsingReg

    using System.Collections.Generic;
    using System.Linq;
    using DataLayer.Models;
    using Form115.Infrastructure.Search.Base;
    using System;

    #endregion

    internal class SearchOptionDateDepart : SearchOption {
        private readonly DateTime _dateDepart;

        public SearchOptionDateDepart(SearchBase sb, DateTime dateDepart)
            : base(sb) {
                _dateDepart = dateDepart;
        }

        public override IEnumerable<Produits> GetResult()
        {
            return SearchBase.GetResult()
                            .Where(p => Math.Abs((p.DateDepart - _dateDepart).TotalDays) <= 10
                                                                        && p.DateDepart.CompareTo(DateTime.Now) >= 0);
        }
    }
}