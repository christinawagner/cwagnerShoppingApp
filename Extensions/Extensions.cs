using System.Security.Claims;
using System.Security.Principal;

namespace cwagnerShoppingApp.Extensions
{
    public static class Extensions
    {
        public static string GetFullName(this IIdentity identity)
        {
            var claim = ((ClaimsIdentity)identity).FindFirst("FullName");
            return claim?.Value ?? string.Empty;
        }
    }
}