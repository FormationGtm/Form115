//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Web;

//namespace Form115.Infrastructure.Search.Options
//{
//    #region UsingReg

//    using DataLayer.Models;
//    using Form115.Infrastructure.Search.Base;

//    #endregion

//    internal class SearchOptionBestSort : SearchOption
//    {
//        public SearchOptionBestSort(SearchBase sb)
//            : base(sb)
//        {
//        }

//        public override IEnumerable<Hotels> GetResult()
//        {
//            var db = new Form115Entities();
//            var produits = SearchBase.GetResult();
//            return db.Reservations
//                                         .AsEnumerable()
//                                         .GroupBy(r => r.Produits.Sejours.IdHotel,
//                                                        r => r.Quantity,
//                                                            (key, g) => new { Hotel = produits.Hotel.Distinct().hotels.Where(h => h.IdHotel == key).FirstOrDefault(), SommeRes = g.Sum() })
//                                         .OrderByDescending(o => o.SommeRes)
//                                         .Select(o => o.Hotel)
//                                         .Where(h => h != null)
//                                         .ToList();    
//        }
//    }
//}