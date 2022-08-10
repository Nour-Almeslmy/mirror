using Microsoft.IdentityModel.Tokens;
using SignInProviderBL.TokenHandler;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;

namespace SignInProviderBL.OathHandler
{
    public class OathHandler : IOathHandler
    {
        private readonly ITokenHandler _tokenHandler;

        public OathHandler(ITokenHandler TokenHandler)
        {
            _tokenHandler = TokenHandler;
        }
        public async Task<bool> ValidateToken(string token, string secretKey)
        {
            var TokenHandler = new JwtSecurityTokenHandler();

            var key = _tokenHandler.GetSecurityKey(secretKey);

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

            var TokenValidationResult = await TokenHandler.ValidateTokenAsync(token, ValidationParameters);

            return TokenValidationResult.IsValid;
        }

        public IPrincipal CreatePrincipal(string token)
        {
            try
            {
                var SecurityToken = new JwtSecurityToken(token);

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
