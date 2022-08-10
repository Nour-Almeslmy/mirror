using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.AspNet.Identity;
using SignInProviderBL.TokenHandler;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace SignInProviderBL.SignInProviderBL
{
    public class SignInProviderHelper : ISignInProviderHelper
    {
        private readonly ApplicationUserManager _userManager;
        private readonly ITokenHandler _tokenHandler;
        private ApplicationUser user = null;

        public SignInProviderHelper(ApplicationUserManager UserManager, ITokenHandler TokenHandler)
        {
            _userManager = UserManager;
            _tokenHandler = TokenHandler;
        }

        private async Task<ApplicationUser> GetUser(string userName)
        {
            if(user is null)
            {
                user = await _userManager.FindByNameAsync(userName);
                return user;
            }

            return user; 
        }

        public async Task<bool> Register(RegisterDTO registerDTO)
        {
            #region Create User
            var user = new ApplicationUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email,
                Role = registerDTO.Role
            };

            try
            {
                var creationResult = await _userManager.CreateAsync(user, registerDTO.Password);
                if(!creationResult.Succeeded) return false;
            }
            catch
            {
                return false;
            }
            #endregion

            #region Add Claims
            var claimsList = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, user.UserName),
                new Claim (ClaimTypes.Email, user.Email),
                new Claim (ClaimTypes.Role, user.Role)
            };
            try
            {
                for (int i = 0; i < claimsList.Count; i++)
                {
                    var creatingClaimsResult = await _userManager.AddClaimAsync(user.Id, claimsList[i]);

                    if (!creatingClaimsResult.Succeeded)
                    {
                        await _userManager.DeleteAsync(user);
                        return false;
                    }
                }
            }
            catch
            {
                await _userManager.DeleteAsync(user);
                return false;
            }
            #endregion

            return true;
        }


        public async Task<SignInProviderResponse> CheckUserCredentials(LoginDTO loginDTO)
        {
            try
            {
                //var user = await _userManager.FindByNameAsync(loginDTO.UserName);
                var user = await GetUser(loginDTO.UserName);

                if (user is null)
                {
                    var response = new SignInProviderResponse(false, "User Name is not registered");
                    return response; 
                }

                var isLocked = await _userManager.IsLockedOutAsync(user.Id);
                if (isLocked)
                {
                    var response = new SignInProviderResponse(false, "You're Locked. Please try again later");
                    return response;
                }

                var isAuthenticated = await _userManager.CheckPasswordAsync(user, loginDTO.Password);
                if (!isAuthenticated)
                {
                    await _userManager.AccessFailedAsync(user.Id);
                    var response = new SignInProviderResponse(false, "Wrong Credentials");
                    return response;
                }

                var response_ = new SignInProviderResponse(true, string.Empty);
                return response_;
            }
            catch
            {
                var response = new SignInProviderResponse(false, "Wrong Credentials");
                return response;
            }
        }

        public async Task<TokenDTO> GenerateToken(string userName, string secretKey)
        {
            try
            {
                //var user = await _userManager.FindByNameAsync(userName);

                var user = await GetUser(userName);

                var userClaims = await _userManager.GetClaimsAsync(user.Id);

                var ResultToken = _tokenHandler.GenerateToken(secretKey, userClaims.ToList(), Guid.NewGuid());

                #region Update User With Refresh Token

                user.RefreshToken = ResultToken.RefreshToken;
                user.RefreshTokenExpiryDate = DateTime.Now.AddMonths(1);

                var updatingResult = await _userManager.UpdateAsync(user);

                if (!updatingResult.Succeeded)
                {
                    return null;
                }
                #endregion
            
                return ResultToken;
            }
            catch
            {
                return null;
            }
        }

        public async Task<SignInProviderResponse> ValidateToken(string userName, Guid refreshToken)
        {
            //var user = await _userManager.FindByNameAsync(userName);

            var user = await GetUser(userName);

            if (user is null)
            {
                var response = new SignInProviderResponse(false, "Wrong Token Credentials");
                return response;
            }

            if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryDate < DateTime.Now)
            {
                var response = new SignInProviderResponse(false, "Invalid Refresh Token");
                return response;
            }

            var response_ = new SignInProviderResponse(true, string.Empty);
            return response_;
        }

        public async Task<TokenDTO> GenerateRefreshToken(string userName, string token, string secretKey, bool rotationalToken)
        {
            //var user = _userManager.FindByName(userName);

            var user = await GetUser(userName);

            var SecurityToken = new JwtSecurityToken(token);

            var SecurityTokenClaims = SecurityToken.Claims.ToList();

            var refreshToken = rotationalToken ? Guid.NewGuid() : user.RefreshToken;

            var resultToken = _tokenHandler.GenerateToken(secretKey, SecurityTokenClaims, refreshToken);

            user.RefreshTokenExpiryDate = DateTime.Now.AddMonths(1);

            if (rotationalToken)
                user.RefreshToken = resultToken.RefreshToken;

            var updatingResult = _userManager.Update(user);

            if (!updatingResult.Succeeded)
                return null;

            return resultToken;

        }

        public async Task<bool> RemoveUserRefreshToken(string userName)
        {
            //var user = await _userManager.FindByNameAsync(userName);
            var user = await GetUser(userName);
            if (user is null)
                return false;

            user.RefreshToken = Guid.Empty;
            user.RefreshTokenExpiryDate = DateTime.Now;

            var updatingResult = await _userManager.UpdateAsync(user);

            return updatingResult.Succeeded;
        }   
    
        public async Task<bool> RevokeToken(string userName)
        {
            try
            {
                //var user = await _userManager.FindByNameAsync(userName);
                var user = await GetUser(userName);
                if (user is null)
                    return false;

                user.RefreshToken = Guid.Empty;
                user.RefreshTokenExpiryDate = DateTime.Now;

                await _userManager.UpdateAsync(user);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}
