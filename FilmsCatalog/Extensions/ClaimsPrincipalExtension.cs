using System.Linq;
using System.Security.Claims;

namespace FilmsCatalog.Extensions
{
    public static class ClaimsPrincipalExtension
    {
        public static string UserId(this ClaimsPrincipal principal) => principal.Claims
            .Union(principal.Identities.SelectMany(x => x.Claims))
            .FirstOrDefault(x => x.Type.Equals(ClaimTypes.NameIdentifier))?.Value;
    }
}