using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.IdentityModel.Tokens;
using SignInProvider.CustomFilters;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using System.Web.Http.Description;

namespace SignInProvider.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);


        private readonly ApplicationUserManager _UserManager;
        public UserController(ApplicationUserManager UserManager)
        {
            _UserManager = UserManager;
        }

        #region Register
        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterDTO registerDTO)
        {

            var user = new ApplicationUser
            {
                UserName = registerDTO.Username,
                Email = registerDTO.Email,
                Role = registerDTO.Role
            };

            #region Try to Create the user
            try
            {
                var creationResult = await _UserManager.CreateAsync(user, registerDTO.Password);
                if (!creationResult.Succeeded)
                {
                    return Ok(creationResult.Errors);
                }
            }
            catch (Exception e)
            {
                log.Error(e.Message);
                return InternalServerError();
            }
            #endregion

            var claimsList = new List<Claim>
            {
                new Claim (ClaimTypes.NameIdentifier, user.UserName),
                new Claim (ClaimTypes.Email, user.Email),
                new Claim (ClaimTypes.Role, user.Role)
            };

            #region Add Claims to the user
            try
            {
                for (int i = 0; i < claimsList.Count; i++)
                {
                    var creatingClaimsResult = await _UserManager.AddClaimAsync(user.Id, claimsList[i]);

                    if (!creatingClaimsResult.Succeeded)
                    {
                        //await DeleteUserClaims(user.Id, claimsList, i);
                        await _UserManager.DeleteAsync(user);
                        return InternalServerError();
                    }
                }
            }
            catch (Exception e)
            {
                await _UserManager.DeleteAsync(user);
                log.Error(e.Message);
                return InternalServerError();
            }
            #endregion

            return Ok("Successfully created");

        }
        #endregion

        #region Login
        [HttpGet, ResponseType(typeof(TokenDTO))]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(LoginDTO loginDTO)
        {
            try
            {
                var user = await _UserManager.FindByNameAsync(loginDTO.UserName);
                if (user is null)
                {
                    return Content(HttpStatusCode.Unauthorized, "User Name is not registered");
                }

                var isLocked = await _UserManager.IsLockedOutAsync(user.Id);
                if (isLocked)
                {
                    return Content(HttpStatusCode.Unauthorized, "You're Locked. Please try again later");
                }

                var isAuthenticated = await _UserManager.CheckPasswordAsync(user, loginDTO.Password);
                if (!isAuthenticated)
                {
                    await _UserManager.AccessFailedAsync(user.Id);
                    return Content(HttpStatusCode.Unauthorized, "Wrong Credentials");
                }

                var userClaims = await _UserManager.GetClaimsAsync(user.Id);

                var ResultToken = GenerateToken(userClaims.ToList(), Guid.NewGuid());

                user.RefreshToken = ResultToken.RefreshToken;
                user.RefreshTokenExpiryDate = DateTime.Now.AddMonths(1);

                var updatingResult = await _UserManager.UpdateAsync(user);

                if (!updatingResult.Succeeded)
                {
                    return InternalServerError(new Exception("Something Went Wrong, Try Again In A Few Minutes ..."));
                }

                return Ok(ResultToken);
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                return Content(HttpStatusCode.Unauthorized, "Wrong Credentials");
            }

        }
        #endregion

        #region RefreshToken
        [BasicAuthentication_ValidToken]
        [HttpPost, ResponseType(typeof(TokenDTO))]
        [Route("RefreshToken")]
        public async Task<IHttpActionResult> RefreshToken([FromBody]Guid refreshToken)
        {
            #region Validation
            //var userName = Thread.CurrentPrincipal.Identity.Name;
            var userName = HttpContext.Current.User.Identity.Name; //user is the current principal

            var user = await _UserManager.FindByNameAsync(userName);
            if (User is null)
            {
                return BadRequest("Wrong Token Credentials");
            }

            if (user.RefreshToken != refreshToken || user.RefreshTokenExpiryDate < DateTime.Now)
            {
                return BadRequest("Invalid Token");
            }
            #endregion

            #region GerateToken
            var requestToken = Request.Headers.Authorization?.Parameter;

            if(requestToken is null)
            {
                return BadRequest("Invalid Token");
            }

            var SecurityToken = new JwtSecurityToken(requestToken);

            var SecurityTokenClaims = SecurityToken.Claims.ToList();

            var generatedToken = GenerateToken(SecurityTokenClaims, user.RefreshToken);

            user.RefreshTokenExpiryDate = DateTime.Now.AddMonths(1);

            var updatingResult = await _UserManager.UpdateAsync(user);

            if(!updatingResult.Succeeded)
            {
                return InternalServerError(new Exception("Something Went Wrong, Try Again In A Few Minutes ..."));
            }

            return Ok(generatedToken);
            #endregion
        }
        #endregion

        #region GenerateToken
        private TokenDTO GenerateToken(List<Claim> userClaims, Guid refreshToken)
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
        #endregion

        private async Task DeleteUserClaims(string id, List<Claim> claimsList, int cliamsCount)
        {
            for (int i = 0; i < cliamsCount; i++)
            {
                await _UserManager.RemoveClaimAsync(id, claimsList[i]);
            }
        }
    }
}
