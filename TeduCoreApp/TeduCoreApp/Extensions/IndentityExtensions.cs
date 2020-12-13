using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace TeduCoreApp.Extensions
{
    public static class IndentityExtensions
    {
        public static string GetSpecificClaim(this ClaimsPrincipal claimsPrincipal, string ClaimType)
        {
            var claim = claimsPrincipal.Claims.FirstOrDefault(X => X.Type == ClaimType);
            return (claim != null) ? claim.Value : string.Empty;
        }
    }
}
