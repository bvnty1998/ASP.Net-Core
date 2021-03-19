using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.System;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class RoleController : BaseController
    {
        private IFunctionService _functionService;
        private IRoleService _roleService;
        private IPermissionService _permissionService;
        public RoleController (IRoleService roleService, IFunctionService functionViewModel,IPermissionService permissionService)
        {
            _functionService = functionViewModel;
            _roleService = roleService;
            _permissionService = permissionService;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// get all roles
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAll()
        {
            var rs = await _roleService.GetAllAsync();
            return new OkObjectResult(rs);
        }
        /// <summary>
        /// Get all role and paging 
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="pageCurrent"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllPagingAsync(string keyWord,int pageCurrent,int pageSize)
        {
            var rs = _roleService.GetAllPagingAsync(keyWord, pageCurrent, pageSize);
            return new OkObjectResult(rs);
        }
        
        /// <summary>
        /// Update and Create a role
        /// </summary>
        /// <param name="appRoleVM"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> SaveRoleAsync(AppRoleViewModel appRoleVM)
        {
           if(string.IsNullOrEmpty(appRoleVM.id.ToString()))
            {
                var rs = await _roleService.AddRoleAsync(appRoleVM);
                return new OkObjectResult(rs);
            }
           else
            {
                var rs = await _roleService.UpdateRoleAsync(appRoleVM);
                return new OkObjectResult(rs);
            }
        }
        /// <summary>
        /// Delete a Role by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> DeleteAsync(string Id)
        {
            var rs = await _roleService.DeleteRoleAsync(Id);
            return new OkObjectResult(rs);
        }

       public async Task<IActionResult> GetRoleByIdAsync(string Id)
        {
            var rs = await _roleService.GetRoleByIdAsync(Id);
            return new OkObjectResult(rs);
        }
        /// <summary>
        /// Get list function
        /// </summary>
        /// <returns></returns>
        public IActionResult GetAllFunction()
        {
            var rs = _functionService.GetAllFunction();
            return new OkObjectResult(rs);
        }

        /// <summary>
        /// Add permission for role
        /// </summary>
        /// <param name="listPermissionVM"></param>
        /// <returns></returns>
        public IActionResult AddPermission(List<PermissionViewModel> listPermissionVM)
        {
            var rs = _permissionService.AddPermission(listPermissionVM);
            return new OkObjectResult(rs);
        }

        public IActionResult GetPermissonByRoleId(string roleId)
        {
            var rs = _permissionService.GetPermissonByRoleId(roleId);
            return new OkObjectResult(rs);
        }

        public IActionResult DeletePermission(string roleId, List<PermissionViewModel> listPermissionVM)
        {
            var rs = _permissionService.DeletePermission(roleId, listPermissionVM);
            return new OkObjectResult(rs);
        }
    }
}