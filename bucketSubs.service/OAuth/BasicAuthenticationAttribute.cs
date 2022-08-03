using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;

namespace BusinessLogicLayer.CustomFilters
{
    public class BasicAuthenticationAttribute : AuthorizationFilterAttribute
    {
        public async override void OnAuthorization(HttpActionContext actionContext)
        {
            if (await AuthorizeRequest_ValidToken(actionContext))
                return;
            else
                actionContext.Response = 
                    actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized);
        }

        #region AuthorizeRequest
        /*
        private ApplicationUserManager UserManager
        {
            get
            {
                return HttpContext.Current.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
        }
        private async Task<bool> AuthorizeRequest(HttpActionContext actionContext)
        {
            try
            {
                var RequestToken = actionContext.Request.Headers.Authorization?.Parameter;
                var requestScheme = actionContext.Request.Headers.Authorization.Scheme; //Bearer

                var SecurityToken = new JwtSecurityToken(RequestToken);

                var SecurityTokenClaims = SecurityToken.Claims.ToList();

                var UserEmail = SecurityTokenClaims.Find(cl => cl.Type == ClaimTypes.Email).Value;

                var expireDateUnixString = SecurityTokenClaims.Find(cl => cl.Type == "exp").Value;

                var SecurityTokenExpireDate = DateTimeOffset.FromUnixTimeSeconds(long.Parse(expireDateUnixString)).LocalDateTime;

                var User = await UserManager.FindByEmailAsync(UserEmail);

                var UserClaims = await UserManager.GetClaimsAsync(User.Id);

                var ExpectedToken = GenerateToken(UserClaims.ToList(), SecurityTokenExpireDate).Token;

                if(ExpectedToken == RequestToken)
                {
                    var identity = new GenericIdentity(User.UserName);
                    IPrincipal principal = new GenericPrincipal(identity, null);

                    if(HttpContext.Current != null)
                    {
                        HttpContext.Current.User = principal;
                    }

                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch
            {
                return false;
            }
        }
        */
        #endregion
        #region GenerateToken
        private TokenDTO GenerateToken(List<Claim> userClaims, DateTime? exp)
        {
            #region Getting Secret Key ready
            var secretKey = ConfigurationManager.AppSettings["SecretKey"];
            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(secretKeyInBytes);
            //var key = JWTHelper.GetKey();
            #endregion

            #region Combining Secret Key with Hashing Algorithm(Hashing Result)
            ///install-package microsoft.identitymodel.tokens 
            var methodUsedInGeneratingToken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            #endregion

            #region Putting all together (Define the token)
            ///install-package System.IdentityModel.Tokens.Jwt

            var jwt = new JwtSecurityToken(
                claims: userClaims,
                expires: exp ?? DateTime.Now.AddMinutes(15),
                notBefore: DateTime.Now,
                signingCredentials: methodUsedInGeneratingToken);
            #endregion

            #region Generate the defined Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var resultToken = tokenHandler.WriteToken(jwt);
            #endregion

            return new TokenDTO
            {
                Token = resultToken,
                ExpiryDate = jwt.ValidTo
            };
        }
        #endregion
        private async Task<bool> AuthorizeRequest_ValidToken(HttpActionContext actionContext)
        {
            var RequestToken = actionContext.Request.Headers.Authorization?.Parameter;
            //var requestScheme = actionContext.Request.Headers.Authorization?.Scheme; //Bearer
           
            if(RequestToken is null)
            {
                return false;
            }

            var TokenHandler = new JwtSecurityTokenHandler();
            //var SecurityToken = TokenHandler.ReadToken(RequestToken);
            //var canValidate = TokenHandler.CanValidateToken;

            //Generate security key
            var secretKey = ConfigurationManager.AppSettings["SecretKey"];
            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
            var key = new SymmetricSecurityKey(secretKeyInBytes);

            var ValidationParameters = new TokenValidationParameters()
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = key,
                ValidateIssuer = true,
                ValidIssuer = "SignIn_Provider",
                ValidateAudience = true,
                ValidAudience = "BucketSubs_Service",
                ValidateLifetime = true
            };

            //TokenHandler.ValidateToken(RequestToken, ValidationParameters, out SecurityToken ValidatedToken);
            //var securityToken = ValidatedToken as JwtSecurityToken;
            //var claims = securityToken.Claims;
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

    }
}
