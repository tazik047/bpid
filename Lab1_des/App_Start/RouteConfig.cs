using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;

namespace Lab1_des
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");

            routes.MapRoute(
                name: "MyPhoto",
                url: "myphoto",
                defaults: new { controller = "Home", action = "CurrentUserPhoto" });

            routes.MapRoute(
                name: "UserChat",
                url: "{id}/chat",
                defaults: new { controller = "Home", action = "Chat" });

            routes.MapRoute(
                name: "UserPhoto",
                url: "{email}/photo",
                defaults: new { controller = "Home", action = "Photo" });

            routes.MapRoute(
                name: "GoogleSign",
                url: "sign-in-google",
                defaults: new { controller = "Account", action = "LoginByGoogle" });

            routes.MapRoute(
                name: "Default",
                url: "{controller}/{action}/{id}",
                defaults: new { controller = "Home", action = "Index", id = UrlParameter.Optional }
            );
        }
    }
}
