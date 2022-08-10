using System.Security.Principal;
using System.Threading.Tasks;

namespace SignInProviderBL.OathHandler
{
    public interface IOathHandler
    {
        Task<bool> ValidateToken(string token, string secretKey);

        IPrincipal CreatePrincipal(string token);
    }
}