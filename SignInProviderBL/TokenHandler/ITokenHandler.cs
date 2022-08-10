using DataAccessLayer.DTOs;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Security.Claims;

namespace SignInProviderBL.TokenHandler
{
    public interface ITokenHandler
    {
        TokenDTO GenerateToken(string secretKey, List<Claim> userClaims, Guid refreshToken);

        SecurityKey GetSecurityKey(string secretKey);
    }
}