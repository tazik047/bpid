using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Web;
using System.Web.Mvc;

namespace Lab1_des.Controllers
{
    [Authorize]
    public abstract class BaseController : Controller
    {
        public ClaimsPrincipal CurrentPrincipal
        {
            get { return HttpContext.User as ClaimsPrincipal; }
        }

        public string CurrentUserEmail
        {
            get { return CurrentPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value; }
        }
    }
}