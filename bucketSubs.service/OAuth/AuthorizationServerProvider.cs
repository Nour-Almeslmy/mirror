using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security.OAuth;
using System.Security.Claims;
using System.Threading.Tasks;

namespace bucketSubs.service.App_Start
{
    public class AuthorizationServerProvider : OAuthAuthorizationServerProvider
    {
        /*
        //pass [authenticate]
        public override async Task ValidateClientAuthentication
            (OAuthValidateClientAuthenticationContext context)
        {
            //if(context.TryGetBasicCredentials(out string clientId, out string clientSecret))

            //validate client credentials
            context.Validated();
        }

        //login - authenticate
        public override async Task GrantResourceOwnerCredentials
            (OAuthGrantResourceOwnerCredentialsContext context)
        {
            //    UserManager<IdentityUser> userManager = context.OwinContext.GetUserManager<UserManager<IdentityUser>>();

            //userManager.GetClaimsAsync

            var identity = new ClaimsIdentity(context.Options.AuthenticationType);

            if(context.UserName == "admin" && context.Password == "admin")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "admin"));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.UserName));
                context.Validated(identity);
            }
            else if(context.UserName == "user" && context.Password == "user")
            {
                identity.AddClaim(new Claim(ClaimTypes.Role, "user"));
                identity.AddClaim(new Claim(ClaimTypes.NameIdentifier, context.UserName));
                context.Validated(identity);
            }
            else
            {
                context.SetError("invalid_granttttt", "provided user name or password is incorrect");
            }
        }
        */
    }
}