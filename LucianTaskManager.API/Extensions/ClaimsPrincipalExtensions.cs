using System.Security.Claims;

namespace LucianTaskManager.Extensions
{
    public static class ClaimsPrincipalExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var userIdClaim = user.FindFirst("userId")?.Value;
            return int.Parse(userIdClaim);
        }
    }
}