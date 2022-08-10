using DataAccessLayer.DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SignInProviderBL.TokenHandler
{
    public class TokenHandler : ITokenHandler
    {
        public TokenDTO GenerateToken(string secretKey, List<Claim> userClaims, Guid refreshToken)
        {
            #region Getting Secret Key ready
            var key = GetSecurityKey(secretKey);
            #endregion

            #region Combining Secret Key with Hashing Algorithm(Hashing Result)
            var methodUsedInGeneratingToken = new SigningCredentials(key, SecurityAlgorithms.HmacSha256Signature);
            #endregion

            #region Putting all together (Define the token)
            var jwt = new JwtSecurityToken(
                claims: userClaims,
                expires: DateTime.Now.AddMinutes(15),
                notBefore: DateTime.Now,
                issuer: "SignIn_Provider",
                audience: "BucketSubs_Service",
                signingCredentials: methodUsedInGeneratingToken);
            #endregion

            #region Generate the defined Token
            var tokenHandler = new JwtSecurityTokenHandler();
            var resultToken = tokenHandler.WriteToken(jwt);
            #endregion

            return new TokenDTO
            {
                Token = resultToken,
                ExpiryDate = jwt.ValidTo,
                RefreshToken = refreshToken
            };
        }

        public SecurityKey GetSecurityKey(string secretKey) //ConfigurationManager.AppSettings["SecretKey"]
        {
            var secretKeyInBytes = Encoding.ASCII.GetBytes(secretKey);
            return new SymmetricSecurityKey(secretKeyInBytes);
        }


    }
}
