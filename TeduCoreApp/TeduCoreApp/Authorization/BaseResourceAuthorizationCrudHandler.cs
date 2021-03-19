using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Authorization.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Utilities.Constants;

namespace TeduCoreApp.Authorization
{
    
    public class BaseResourceAuthorizationCrudHandler : AuthorizationHandler<OperationAuthorizationRequirement, string>
    {
        IRoleService _roleService;
        public BaseResourceAuthorizationCrudHandler(IRoleService roleService)
        {
            _roleService = roleService;
        }
        protected override async Task  HandleRequirementAsync(AuthorizationHandlerContext context, OperationAuthorizationRequirement requirement, string resource)
        {
            var role = ((ClaimsIdentity)context.User.Identity).Claims.FirstOrDefault(x => x.Type == CommonConstants.UserClaims.Roles);
            if (role != null)
            {
                var listRole = role.Value.Split(";");
                var hasPermission = await _roleService.CheckPermissoion(resource, requirement.Name, listRole);
                if (hasPermission || listRole.Contains(CommonConstants.AdminRole))
                {
                    context.Succeed(requirement);
                }
                else
                {
                    context.Fail();
                }
            }
            else
            {
                context.Fail();
            }
        }
    }
}
