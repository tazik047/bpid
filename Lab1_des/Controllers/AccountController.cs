using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using BLL.Abstract;
using Lab1_des.Models;

namespace Lab1_des.Controllers
{
    public class AccountController : Controller
    {
        private readonly IAuthorizationService _authorizationService;

        public AccountController(IAuthorizationService authorizationService)
        {
            _authorizationService = authorizationService;
        }

        public ActionResult Login()
        {
            var google = GetGoogleOauthData();

            return View(google);
        }

        public ActionResult LoginByGoogle(string code)
        {
            if (_authorizationService.Login(code, Server.MapPath("~/Content/assets/img"), GetGoogleOauthData().RedirectUrl))
            {
                return RedirectToAction("Index", "Home");
            }

            return View("Login", GetGoogleOauthData());
        }

        public ActionResult Logout()
        {
            _authorizationService.Logout();

            return RedirectToAction("Login");
        }

        [NonAction]
        private GoogleOauthViewModel GetGoogleOauthData()
        {
            return new GoogleOauthViewModel
            {
                RedirectUrl = Url.Action("LoginByGoogle", "Account", null, Request.Url.Scheme),
                ClientId = "370393427927-eqv21p3qosqkp1uqgjlattejsf1p9b43.apps.googleusercontent.com"
            };
        }
    }
}