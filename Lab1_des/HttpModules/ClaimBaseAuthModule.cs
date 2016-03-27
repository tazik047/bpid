using System;
using System.Security.Claims;
using System.Web;
using BLL.Concrete;
using BLL.Concrete.Utils;
using Microsoft.Owin.Security.DataHandler;

namespace Lab1_des.HttpModules
{
    public class ClaimBaseAuthModule : IHttpModule
    {
        public void Dispose()
        {
        }

        public void Init(HttpApplication context)
        {
            context.PostAuthenticateRequest += ReplacePrincipal;
        }

        private static void ReplacePrincipal(object sender, EventArgs args)
        {
            var cookie = HttpContext.Current.Request.Cookies[AuthorizationService.Cookie];

            if (cookie == null)
            {
                return;
            }

            var ticketFormat = new TicketDataFormat(new DataProtector(AuthorizationService.ProtectionPurpose));
            var ticket = ticketFormat.Unprotect(cookie.Value);

            if (ticket != null)
            {
                var principal = new ClaimsPrincipal(ticket.Identity);
                HttpContext.Current.User = principal;
            }
        }
    }
}