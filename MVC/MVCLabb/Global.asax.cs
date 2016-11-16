using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using MVCLabb.Controllers;
using System.IO;
using MVCLabb.Models;

namespace MVCLabb
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            SeedPhotos();

        }

        protected void Application_Error()
        {
            Response.Redirect("~/Home/Error");
        }

        public void SeedPhotos()
        {
            if (PhotoController.photoes.Count() < 1)
            {
                var photoDir = Directory.GetFiles(Server.MapPath("~/Photos"));

                foreach (var photo in photoDir)
                {
                    var info = new FileInfo(photo);

                    PhotoController.photoes.Add(new PhotoModel { name = info.Name, id = Guid.NewGuid(), path = $" /Photos/{info.Name}", description = "nn" });
                }
            }

        }
    }
}
