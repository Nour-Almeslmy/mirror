using DataAccessLayer.Data;
using DataAccessLayer.DTOs;
using DataAccessLayer.Models;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using System.Web.Http;

namespace bucketSubs.service.Controllers
{
    [RoutePrefix("api/users")]
    public class UserController : ApiController
    {
        private static readonly log4net.ILog log =
            log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        //private ApplicationUserManager UserManager
        //{
        //    get
        //    {
        //        return Request.GetOwinContext().GetUserManager<ApplicationUserManager>();
        //    }
        //}

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
                if(!creationResult.Succeeded)
                {
                    return Ok(creationResult.Errors);
                }
            }
            catch(Exception e)
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
            catch(Exception e)
            {
                await _UserManager.DeleteAsync(user);
                log.Error(e.Message);
                return InternalServerError();
            }
            #endregion

            return Ok("Successfully created");

        }
        #endregion

        [HttpGet]
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

                return Ok(GenerateToken(userClaims.ToList(), null));
            }
            catch (Exception e)
            {
                log.Warn(e.Message);
                return Content(HttpStatusCode.Unauthorized, "Wrong Credentials");
            }

        }

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
                ExpiryDate = jwt.ValidTo
            };
        }

        private async Task DeleteUserClaims(string id, List<Claim> claimsList, int cliamsCount)
        {
            for(int i = 0; i< cliamsCount; i++)
            {
                await _UserManager.RemoveClaimAsync(id, claimsList[i]);
            }
        }
    }

    }
