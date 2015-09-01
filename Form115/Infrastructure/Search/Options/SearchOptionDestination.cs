using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Form115.Infrastructure.Search.Options
{
     #region UsingReg

    using DataLayer.Models;
    using Form115.Infrastructure.Search.Base;

    #endregion

    internal class SearchOptionDestination : SearchOption {

        private readonly int _idContinent;
        private readonly int _idRegion;
        private readonly string _idPays;
        private readonly int _idVille;

        public SearchOptionDestination(SearchBase sb, int idContinent, int idRegion, string idPays, int idVille)
            : base(sb) {
                _idContinent = idContinent;
                _idRegion = idRegion;
                _idPays = idPays;
                _idVille = idVille;
        }

        public override IEnumerable<Produits> GetResult()
        {            
            var db = new Form115Entities();

            if (_idVille != 0)
            {
                return SearchBase.GetResult().Where(p => p.Sejours.Hotels.Villes.idVille == _idVille);
            }
            else if (_idPays != null && _idPays != "0")
            {
                return SearchBase.GetResult()
                                .Where(p => p.Sejours.Hotels.Villes.Pays.CodeIso3 == _idPays);
            }
            else if (_idRegion != 0)
            {
                return SearchBase.GetResult()
                                .Where(p => p.Sejours.Hotels.Villes.Pays.Regions.idRegion == _idRegion);
            }
            else if (_idContinent != 0)
            {
                return SearchBase.GetResult()
                                .Where(p => p.Sejours.Hotels.Villes.Pays.Regions.Continents.idContinent == _idContinent);
            }
            else
            {
                return SearchBase.GetResult();//))
            }
        }
    }
}