using System.Configuration;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using SignInProviderBL.OathHandler;

namespace SignInProvider.OAth
{
    public class BasicAuthentication_ValidToken : AuthorizationFilterAttribute
    {
        public async override void OnAuthorization(HttpActionContext actionContext)
        {
            if (await ValidateToken_mod(actionContext))
                return;
            else
                actionContext.Response = 
                    actionContext.Request.CreateResponse(HttpStatusCode.BadRequest, "Invalid Token");
        }

        private async Task<bool> ValidateToken_mod(HttpActionContext actionContext)
        {
            var RequestToken = actionContext.Request.Headers.Authorization?.Parameter;
            if (RequestToken is null)
                return false;

            var secretKey = ConfigurationManager.AppSettings["SecretKey"];

            var BasicAuth = new BasicAuthentication(RequestToken, secretKey);

            if(await BasicAuth.ValidateToken())
            {
                var Principal = BasicAuth.CreatePrincipal();

                Thread.CurrentPrincipal = Principal;

                if (HttpContext.Current != null)
                {
                    HttpContext.Current.User = Principal;
                }

                return true;
            }
            else
            {
                return false;
            }
        }

        #region Old Validation
        /*
        private async Task<bool> ValidateToken(HttpActionContext actionContext)
        {
            var RequestToken = actionContext.Request.Headers.Authorization?.Parameter;

            if (RequestToken is null)
                return false;


            var TokenHandler = new JwtSecurityTokenHandler();

            #region Generate security key
            var secretKey = ConfigurationManager.AppSettings["SecretKey"];
            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(secretKeyInBytes);
            #endregion

            var ValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = "SignIn_Provider",
                ValidateAudience = true,
                ValidAudience = "BucketSubs_Service",
                ValidateLifetime = false
            };

            var TokenValidationResult = await TokenHandler.ValidateTokenAsync(RequestToken, ValidationParameters);

            if (TokenValidationResult.IsValid)
            {
                #region Create a Principal Object and attach it to the current thread
                var UserName = TokenValidationResult.Claims[ClaimTypes.NameIdentifier].ToString();
                var UserRoles = TokenValidationResult.Claims[ClaimTypes.Role].ToString().Split(',');

                var identity = new GenericIdentity(UserName);

                IPrincipal Principal = new GenericPrincipal(identity, UserRoles);

                Thread.CurrentPrincipal = Principal;

                if(HttpContext.Current != null)
                {
                    HttpContext.Current.User = Principal;
                }
                #endregion

                return true;
            }
            else
                return false;
        }
        */
        #endregion

    }
}
