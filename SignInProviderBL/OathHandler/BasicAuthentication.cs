using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SignInProviderBL.OathHandler
{
    public class BasicAuthentication
    {
        private string Token { get; set; }
        private string SecretKey { get; set; }

        public BasicAuthentication(string token, string secretKet)
        {
            Token = token;
            SecretKey = secretKet;
        }

        public async Task<bool> ValidateToken()
        {
            var TokenHandler = new JwtSecurityTokenHandler();

            var secretKeyInBytes = Encoding.ASCII.GetBytes(SecretKey);
            var key = new SymmetricSecurityKey(secretKeyInBytes);

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

            var TokenValidationResult = await TokenHandler.ValidateTokenAsync(Token, ValidationParameters);

            return TokenValidationResult.IsValid;
        }

        public IPrincipal CreatePrincipal()
        {
            try
            {
                var SecurityToken = new JwtSecurityToken(Token);

                var SecurityTokenClaims = SecurityToken.Claims.ToList();

                var UserName = SecurityTokenClaims.Find(cl => cl.Type == ClaimTypes.NameIdentifier).Value;
                var UserRoles = SecurityTokenClaims.Find(cl => cl.Type == ClaimTypes.Role).Value.Split(',');

                var identity = new GenericIdentity(UserName);

                IPrincipal Principal = new GenericPrincipal(identity, UserRoles);

                return Principal;
            }
            catch
            {
                return null;
            }
        }

    }
}
