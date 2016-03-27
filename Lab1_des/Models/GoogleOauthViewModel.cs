using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Lab1_des.Models
{
    public class GoogleOauthViewModel
    {
        public string ClientId { get; set; }

        public string RedirectUrl { get; set; }

        public string GetUrl
        {
            get
            {
                return
                    string.Format(
                        "https://accounts.google.com/o/oauth2/v2/auth?scope=email profile&redirect_uri={0}&response_type=code&client_id={1}",
                        RedirectUrl,
                        ClientId);
            }
        }
    }
}