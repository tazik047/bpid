using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using AutoMapper;
using BLL.Abstract;
using BLL.Concrete.Utils;
using BLL.Models;
using Microsoft.Owin.Security;
using Microsoft.Owin.Security.DataHandler;
using Models;

namespace BLL.Concrete
{
    public class AuthorizationService: IAuthorizationService
    {
        public const string Cookie = "ChatMTAPZLAB1Auth";
        public const string ProtectionPurpose = "Authorization token";

        #region Google Oauth Data

        private const string GetTokenAddress = "https://www.googleapis.com/oauth2/v4/token";
        private const string ClientId = "370393427927-eqv21p3qosqkp1uqgjlattejsf1p9b43.apps.googleusercontent.com";
        private const string ClientSecret = "n08erjuIFo1RpidmvO0YpbQF";
        
        #endregion
        
        private readonly IUserService _userService;

        public AuthorizationService(IUserService userService)
        {
            _userService = userService;
        }

        public bool Login(string code, string pathToFolder, string redirectUrl)
        {
            var user = AuthorizeInGoogle(code, pathToFolder, redirectUrl);
            if (user == null)
            {
                return false;
            }

            var claims = new[]
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email), 
                new Claim(ClaimTypes.Name, user.Name),
                new Claim(ClaimTypeExtension.Surname, user.Surname)
            };

            var principal = CreatePrincipal(user.Email, claims);

            AddCookieFromIdentity(principal.Identity as ClaimsIdentity);
            HttpContext.Current.User = principal;

            return true;
        }

        public void Logout()
        {
            HttpContext.Current.Response.Cookies[Cookie].Value = string.Empty;
        }

        private User AuthorizeInGoogle(string code, string pathToFolder, string redirectUrl)
        {
            using (var client = new HttpClient())
            {
                var ticket = client.PostAsync(
                    new Uri(GetTokenAddress),
                    new FormUrlEncodedContent(new[]
                    {
                        new KeyValuePair<string, string>("client_id", ClientId),
                        new KeyValuePair<string, string>("redirect_uri", redirectUrl),
                        new KeyValuePair<string, string>("client_secret", ClientSecret),
                        new KeyValuePair<string, string>("code", code),
                        new KeyValuePair<string, string>("grant_type", "authorization_code"),
                    })).Result;
                var token = ticket.Content.ReadAsStringAsync().Result.ToObject<TokenModel>();

                var jwt = new JwtSecurityToken(token.IdToken);
                var payload = jwt.Payload.SerializeToJson().ToObject<Payload>();
                
                var user = _userService.GetUserOrDefault(payload.Email);
                if (user != null)
                {
                    return user;
                }

                client.DefaultRequestHeaders.Authorization = AuthenticationHeaderValue.Parse(token.TokenType + " " + token.AccessToken);
                var googleUserJson = client.GetAsync(new Uri("https://www.googleapis.com/oauth2/v1/userinfo")).Result;
                var googleUser = googleUserJson.Content.ReadAsStringAsync().Result.ToObject<GoogleUser>();

                user = Mapper.Map<User>(googleUser);

                user.ContentType = SavePhotoFromGoogle(client, googleUser, pathToFolder);

                _userService.RegisterUser(user);

                return user;
            }
        }

        private string SavePhotoFromGoogle(HttpClient client, GoogleUser user, string pathToFolder)
        {
            var photo = client.GetAsync(user.Photo).Result;
            var path = string.Format("{0}\\{1}", pathToFolder, user.Email);
            File.WriteAllBytes(path, photo.Content.ReadAsByteArrayAsync().Result);

            return photo.Content.Headers.ContentType.MediaType;
        }

        private ClaimsPrincipal CreatePrincipal(string userName, IEnumerable<Claim> claims)
        {
            var claimsList = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, userName)
            };

            claimsList.AddRange(claims);

            var claimsIdentity = new ClaimsIdentity(claimsList, "Local auth", ClaimTypes.Name, ClaimTypes.Role);

            return new ClaimsPrincipal(claimsIdentity);
        }

        private void AddCookieFromIdentity(ClaimsIdentity identity, DateTime? expirationTime = null)
        {
            var authProperties = new AuthenticationProperties()
            {
                IsPersistent = false,
                ExpiresUtc = expirationTime ?? DateTime.UtcNow.AddHours(24),
                IssuedUtc = DateTime.UtcNow
            };

            var ticket = new AuthenticationTicket(identity, authProperties);

            var ticketFormat = new TicketDataFormat(new DataProtector(ProtectionPurpose));
            var serializedTicked = ticketFormat.Protect(ticket);

            var cookie = new HttpCookie(Cookie, serializedTicked);

            HttpContext.Current.Response.Cookies.Add(cookie);
        }
    }
}
