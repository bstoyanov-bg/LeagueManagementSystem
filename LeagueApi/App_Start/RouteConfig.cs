using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace LeagueApi
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            // Redirect root URL to /swagger for easy testing
            routes.MapRoute(
                name: "SwaggerRedirect",
                url: "",
                defaults: new { controller = "SwaggerRedirect", action = "Index" }
            );
        }
    }
}
