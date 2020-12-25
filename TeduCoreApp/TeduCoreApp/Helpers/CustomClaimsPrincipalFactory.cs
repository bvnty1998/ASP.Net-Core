using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Helpers
{
    public class CustomClaimsPrincipalFactory : UserClaimsPrincipalFactory<AppUser,AppRole>
    {
        UserManager<AppUser> _userManager;
        public CustomClaimsPrincipalFactory(UserManager<AppUser> userManager,RoleManager<AppRole> roleManager
            , IOptions<IdentityOptions> options):base(userManager,roleManager,options)
        {
            _userManager = userManager;
        }
        public async override Task<ClaimsPrincipal> CreateAsync(AppUser User)
        {
            var principal =await base.CreateAsync(User);
            var roles = await _userManager.GetRolesAsync(User);
            ((ClaimsIdentity)principal.Identity).AddClaims(new[]
                {
                new Claim("Email",User.Email),
                new Claim("FullName",User.FullName),
                new Claim("Avatar",User.Avatar ?? string.Empty),
                new Claim("Roles",string.Join(",",roles))
            });
            return principal;

        }
    }
}
