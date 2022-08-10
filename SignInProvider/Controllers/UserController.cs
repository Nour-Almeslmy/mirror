using DataAccessLayer.DTOs;
using SignInProvider.OAth;
using SignInProviderBL.SignInProviderBL;
using System;
using System.Configuration;
using System.Net;
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

        private readonly ISignInProviderHelper _signInProvider;

        public UserController(ISignInProviderHelper signInProvider)
        {
            _signInProvider = signInProvider;
        }

        #region Register
        [HttpPost]
        [Route("Register")]
        public async Task<IHttpActionResult> Register(RegisterDTO registerDTO)
        {
            var RegisterResult = await _signInProvider.Register(registerDTO);
            
            if(RegisterResult)  
                return Ok("Successfully created");
            else 
                return InternalServerError(new Exception("Something Went Wrong! Try Again In A Few Minutes ..."));
        }
        #endregion

        #region Login
        [HttpGet, ResponseType(typeof(TokenDTO))]
        [Route("Login")]
        public async Task<IHttpActionResult> Login(LoginDTO loginDTO)
        {
            var CheckCredentials = await _signInProvider.CheckUserCredentials(loginDTO);
            if (!CheckCredentials.Result)
            {
                return Content(HttpStatusCode.Unauthorized, CheckCredentials.ErrorMessage);
            }

            var secretKey = ConfigurationManager.AppSettings["SecretKey"];

            var resultToken = await _signInProvider.GenerateToken(loginDTO.UserName, secretKey);

            if(resultToken is null)
            {
                return InternalServerError(new Exception("Something Went Wrong, Try Again In A Few Minutes ..."));
            }

            return Ok(resultToken);

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

            var CheckTokenValidation =await _signInProvider.ValidateToken(userName, refreshToken);

            if(!CheckTokenValidation.Result)
            {
                return BadRequest(CheckTokenValidation.ErrorMessage);
            }
            #endregion

            #region GenerateToken

            #region Get Token From Request Header
            var requestToken = Request.Headers.Authorization?.Parameter;
            if(requestToken is null)
                return BadRequest("Invalid Token");
            #endregion

            var secretKey = ConfigurationManager.AppSettings["SecretKey"];

            var resultToken = _signInProvider.GenerateRefreshToken(userName, requestToken, secretKey, false);

            if (resultToken.Result is null)
            {
                return InternalServerError(new Exception("Something Went Wrong, Try Again In A Few Minutes ..."));
            }

            return Ok(resultToken.Result);
            #endregion
        }
        #endregion

        #region Rotational Token - enforcing sender-constraint
        [BasicAuthentication_ValidToken]
        [HttpPost, ResponseType(typeof(TokenDTO))]
        [Route("RotationalToken")]
        public async Task<IHttpActionResult> RotationalToken([FromBody] Guid refreshToken)
        {
            #region Validation
            var userName = HttpContext.Current.User.Identity.Name;

            var CheckTokenValidation = await _signInProvider.ValidateToken(userName, refreshToken);

            if (!CheckTokenValidation.Result && CheckTokenValidation.ErrorMessage == "Invalid Refresh Token")
            { 
                var removeingResult = await _signInProvider.RemoveUserRefreshToken(userName);
                
                if(!removeingResult)
                    return InternalServerError(new Exception("Something Went Wrong, Try Again In A Few Minutes ..."));
                
                return Unauthorized();
            }
            #endregion

            #region GenerateToken

            #region Get Token From Request Header
            var requestToken = Request.Headers.Authorization?.Parameter;
            if (requestToken is null)
                return BadRequest("Invalid Token");
            #endregion

            var secretKey = ConfigurationManager.AppSettings["SecretKey"];

            var resultToken = _signInProvider.GenerateRefreshToken(userName, requestToken, secretKey, false);

            if (resultToken.Result is null)
            {
                return InternalServerError(new Exception("Something Went Wrong, Try Again In A Few Minutes ..."));
            }

            return Ok(resultToken.Result);
            #endregion
        }
        #endregion

        #region RevokeToken
        [HttpDelete]
        //[CustomAuthorize(Roles ="admin")]
        [CustomAuthorize]
        [Route("RevokeToken")]
        public async Task<IHttpActionResult> RevokeToken(string userName)
        {
            var revokeResult = await _signInProvider.RevokeToken(userName);

            if (revokeResult)
                return Ok();

            return BadRequest();
        }
        #endregion
    }
}
