namespace Form115.Infrastructure.Search
{
    #region UsingReg

    using System.Collections.Generic;
    using DataLayer.Models;
    using Form115.Infrastructure.Search.Base;

    #endregion

    internal class Search : SearchBase {
        public Search() {
            SearchResults = new Form115Entities().Produits;
        }


        public Search(IEnumerable<Produits> result)
        {
            SearchResults = result;
        }

        public override IEnumerable<Produits> GetResult()
        {
            return SearchResults;
        }
    }
}