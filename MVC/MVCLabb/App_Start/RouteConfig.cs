using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace MVCLabb
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            

            routes.MapRoute(
                 "Default", // Route name
                 "{controller}/{action}/{id}", // URL with parameters
                    new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                    new string[] { "MVCLabb.Controllers" }
                    );

            routes.MapRoute(
                 "Admin",
                 "admin/",
                  new { controller = "Home", action = "Index", id = UrlParameter.Optional }, // Parameter defaults
                    new string[] { "MVCLabb.Areas.Admin.Controllers" }
                    );




        }
    }
}
