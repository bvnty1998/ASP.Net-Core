using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.System;

using TeduCoreApp.Utilities.Constants;

namespace TeduCoreApp.Areas.Admin.Components
{
    public class SideBarViewComponent :ViewComponent
    {
        private IFunctionService _functionService;
        public SideBarViewComponent(IFunctionService functionService)
        {
            _functionService = functionService;

        }
        public async Task<IViewComponentResult> InvokeAsync()
        {
            var role = UserClaimsPrincipal.Claims?.FirstOrDefault(x => x.Type == "Roles").Value;
            
            List<FunctionViewModel> functions;
            if (role.Split(";").Contains(CommonConstants.AdminRole))
            {
                functions = await _functionService.GetAll();
            }
            else
            {
                //TODO: Get by Permission
                functions = new List<FunctionViewModel>();
            }
            return View(functions);
        }
        
    }
}
