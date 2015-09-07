using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Form115
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                        "HotelDetailsPeriode",                                           // Route name
                "Hotel/Details/{id}/{startDate}/{endDate}",       // URL with parameters
                new { controller = "Hotel", action = "DetailsPeriode" }  // Parameter defaults
            );

            routes.MapRoute("Promotions", "Promotions/{action}/{id}",
            new { controller = "Promotions", action = "Index", id = UrlParameter.Optional },
            new[] { "Form115.Controllers" });


            routes.MapRoute("Home", "Home/{action}/{id}",
            new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            new[] { "Form115.Controllers" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }

}
