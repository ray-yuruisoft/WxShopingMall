using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Yuruisoft.RS.Web
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            //routes.IgnoreRoute("{Scripts}/{*pathInfo}");
            //routes.IgnoreRoute("{Content}/{*pathInfo}");
            //routes.IgnoreRoute("{Theme}/{*pathInfo}");

            //routes.MapRoute(name: "uploadFiles",
            //      url: "Theme/{*filename}",
            //      defaults: new { controller = "StaticFile", action = "Index" });


            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Default", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}