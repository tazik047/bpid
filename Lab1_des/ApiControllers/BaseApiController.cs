using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web;
using System.Web.Http;

namespace Lab1_des.ApiControllers
{
    [Authorize]
    public abstract class BaseApiController : ApiController
    {
        public ClaimsPrincipal CurrentPrincipal
        {
            get { return HttpContext.Current.User as ClaimsPrincipal; }
        }

        public string CurrentUserEmail
        {
            get { return CurrentPrincipal.FindFirst(ClaimTypes.NameIdentifier).Value; }
        }
    }
}
