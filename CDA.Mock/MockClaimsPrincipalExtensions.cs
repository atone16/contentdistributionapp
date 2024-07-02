using System.Security.Claims;

namespace CDA.Mock
{
    public static class MockClaimsPrincipalExtensions
    {
        public static Guid GetTenantId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse("ee4a88f9-f2cc-4bf0-9cbf-2e0d2d798db7");
        }

        public static Guid GetUserId(this ClaimsPrincipal claimsPrincipal)
        {
            return Guid.Parse("1658272f-b742-4fcf-91fa-638575ca6503");
        }
    }
}
