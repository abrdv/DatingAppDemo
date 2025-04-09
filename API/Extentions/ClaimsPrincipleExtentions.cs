using System.Security.Claims;

namespace API.Extentions
{
    public static class ClaimsPrincipleExtentions
    {
        public static string GetUsername(this ClaimsPrincipal claimsPrincipal)
        {
            var username = claimsPrincipal.FindFirstValue(ClaimTypes.NameIdentifier);
            if (username == null) throw new Exception("Username not found from token");
            return username;
        }
    }
}
