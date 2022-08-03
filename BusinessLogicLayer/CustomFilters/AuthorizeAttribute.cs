using System;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Web;
using System.Web.Http.Controllers;
using DataAccessLayer.Data;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BusinessLogicLayer.CustomFilters
{
    public class MyAuthorizeAttribute : System.Web.Http.AuthorizeAttribute
    {
        protected override void HandleUnauthorizedRequest(HttpActionContext actionContext)
        {
            if(HttpContext.Current.User.Identity.IsAuthenticated)
            {
                base.HandleUnauthorizedRequest(actionContext);
            }
            else
            {
                actionContext.Response = new HttpResponseMessage(HttpStatusCode.Forbidden);
            }
        }

        public override void OnAuthorization(HttpActionContext actionContext)
        {
            var requestToken = actionContext.Request.Headers.Authorization.Parameter;
            var requestScheme = actionContext.Request.Headers.Authorization.Scheme; //Bearer

            #region Method JwtSecurityToken Class - exp problem - generate token with the old exp
            /*
            var token = new JwtSecurityToken(requestToken);

            var tokenClaims = token.Claims.ToList();

            var emailClaim = tokenClaims.Find(cl => cl.Type == ClaimTypes.Email);

            var email = emailClaim.Value;

            var owinContext = HttpContext.Current.GetOwinContext();
            */
            #endregion

            #region Method JwtSecurityTokenHandler Class
            /*
            var TokenHandler = new JwtSecurityTokenHandler();

            var SecurityToken = TokenHandler.ReadToken(requestToken);

            var secretKey = ConfigurationManager.AppSettings["SecretKey"];
            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(secretKeyInBytes);

            var ValidationParameters = new TokenValidationParameters()
            {
                RequireExpirationTime = true,
                TokenDecryptionKey = key,
                IssuerSigningKey = key,
                ValidateAudience = true,
                RequireAudience = false
            };

            SecurityToken sectok;

            var IsValidToken = TokenHandler.ValidateToken(requestToken, ValidationParameters, out sectok);
            */
            #endregion




            /*
            var requestTokenClaims = Convert.FromBase64String(requestToken.Split('.')[1]);
            var requestTokenClaimsJSON = JsonConvert.DeserializeObject(requestTokenClaims.ToString());
            */
            base.OnAuthorization(actionContext);
        }

    }
}
