using Microsoft.IdentityModel.Tokens;
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

namespace bucketSubs.service.OAuth
{
    public class BasicAuthenticationMessageHandler : DelegatingHandler
    {
        protected async override Task<HttpResponseMessage> SendAsync
            (HttpRequestMessage request, CancellationToken cancellationToken)
        {
            try
            {
                #region Get the token from the request header
                var RequestToken = request.Headers.Authorization?.Parameter;

                if (RequestToken is null)
                {
                    return await AbortRequest();
                }
                #endregion

                # region Generate security key
                var secretKey = ConfigurationManager.AppSettings["SecretKey"];
                var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
                var key = new SymmetricSecurityKey(secretKeyInBytes);
                #endregion

                #region Validate Token
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

                var TokenHandler = new JwtSecurityTokenHandler();
                var TokenValidationResult = await TokenHandler.ValidateTokenAsync(RequestToken, ValidationParameters);
                #endregion
            
                if (TokenValidationResult.IsValid)
                {
                    #region Create a Principal Object and attach it to the current thread
                    var UserName = TokenValidationResult.Claims[ClaimTypes.NameIdentifier].ToString();
                    var UserRoles = TokenValidationResult.Claims[ClaimTypes.Role].ToString().Split(',');

                    var identity = new GenericIdentity(UserName);

                    IPrincipal Principal = new GenericPrincipal(identity, UserRoles);

                    Thread.CurrentPrincipal = Principal;

                    if (HttpContext.Current != null)
                    {
                        HttpContext.Current.User = Principal;
                    }
                    #endregion

                    return await base.SendAsync(request, cancellationToken);
                }
                else
                {
                    return await AbortRequest();
                }
            }
            catch
            {
                return await AbortRequest();
            }
        }

        private static async Task<HttpResponseMessage> AbortRequest()
        {
            var response = new HttpResponseMessage(HttpStatusCode.Unauthorized);
            var tsc = new TaskCompletionSource<HttpResponseMessage>();
            tsc.SetResult(response);
            return await tsc.Task;
        }
    }
}