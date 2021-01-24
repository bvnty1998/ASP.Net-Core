using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Applications.Interfaces;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        private IRoleService _roleService; 
        public RoleController (IRoleService roleService)
        {
            _roleService = roleService;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult GetAll()
        {
            var rs = _roleService.GetAllAsync();
            return new OkObjectResult(rs);
        }
    }
}