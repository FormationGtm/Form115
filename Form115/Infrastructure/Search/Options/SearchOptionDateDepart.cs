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
        private readonly DateTime _dateDebut;
        private readonly DateTime _dateFin;

        public SearchOptionDateDepart(SearchBase sb, DateTime dateDebut, DateTime dateFin)
            : base(sb) {
                _dateDebut = dateDebut;
                _dateFin = dateFin;
        }

        public override IEnumerable<Produits> GetResult()
        {
            return SearchBase.GetResult()
                            .Where(p => p.DateDepart >= _dateDebut && p.DateDepart <= _dateFin);
        }
    }
}