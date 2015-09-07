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
        private readonly byte? _dureeMin;
        private readonly byte? _dureeMax;

        public SearchOptionDuree(SearchBase sb, byte? dureeMin, byte? dureeMax)
            : base(sb)
        {
            _dureeMin = dureeMin;
            _dureeMax = dureeMax;
        }

        public override IEnumerable<Produits> GetResult()
        {
            // TODO Mieux tester les données entrantes
            return _dureeMin.HasValue && _dureeMax.HasValue
                ? SearchBase.GetResult()
                            .Where(p => p.Sejours.Duree >= _dureeMin &&
                                        p.Sejours.Duree <= _dureeMax)
                : SearchBase.GetResult();
        }
    }
}