using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Form115.Infrastructure.Filters
{
    using System.Web.Mvc;
    using DataLayer.Models;

    public class HotelTrackerFilterAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            var db = new Form115Entities();

            var ap = filterContext.ActionParameters.First();

            var ht = new HotelTracking
            {
                DateHT = DateTime.Now,
                IdHotel = (int)ap.Value
            };

            db.HotelTracking.Add(ht);

            db.SaveChanges();
        }
    }
}