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
                name: "KeyChat",
                url: "{id}/simplekeychat",
                defaults: new { controller = "Home", action = "KeyChat" });

            routes.MapRoute(
                name: "RsaChat",
                url: "{id}/rsachat",
                defaults: new { controller = "Home", action = "RsaChat" });

            routes.MapRoute(
                name: "DiffyChat",
                url: "{id}/Diffychat",
                defaults: new { controller = "Home", action = "DiffyChat" });

            routes.MapRoute(
                name: "UserChat",
                url: "{id}/deschat",
                defaults: new { controller = "Home", action = "DesChat" });

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
