using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using System;
using System.Threading.Tasks;

namespace SignInProviderBL.SignInProviderBL
{
    public interface ISignInProviderHelper
    {
        Task<bool> Register(RegisterDTO registerDTO);

        Task<SignInProviderResponse> CheckUserCredentials(LoginDTO loginDTO);

        Task<TokenDTO> GenerateToken(string userName, string secretKey);

        Task<SignInProviderResponse> ValidateRefreshToken(string userName, Guid refreshToken);

        Task<TokenDTO> GenerateRefreshToken(string userName, string token, string secretKey, bool rotationalToken);

        Task<bool> RemoveUserRefreshToken(string userName);

        Task<bool> RevokeToken(string userName);
    }
}