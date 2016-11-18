using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MVCLabb.Controllers;
using System.IO;
using MVCLabb.Models;
using MVCLabb.App_Start;
using System.Web.Helpers;
using System.Security.Claims;

namespace MVCLabb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            AntiForgeryConfig.UniqueClaimTypeIdentifier = ClaimTypes.NameIdentifier;
           

        }

        //protected void Application_Error()
        //{
        //    Response.Redirect("~/Home/Error");
        //}

        
    }
}
