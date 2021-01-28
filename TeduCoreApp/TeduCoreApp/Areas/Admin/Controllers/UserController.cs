﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.System;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class UserController : BaseController
    {
        private IUserService _userService;
        public UserController(IUserService userService)
        {
            _userService = userService;
        }
        public IActionResult Index()
        {
            return View();
        }
        // get list user 
        [HttpGet]
        public IActionResult GetAll(string keyword, int page, int pageSize)
        {
            var model = _userService.GetAllPagingAsnync(keyword,page,pageSize);
            return new OkObjectResult(model);

        }
        // find user by id
        [HttpPost]
        public async Task<IActionResult> FindById(string id)
        {
            var user = await _userService.GetById(id);
            return new OkObjectResult(user);
        }
        // save data when admin add or edit user
        [HttpPost]
        public async Task<IActionResult> Save(AppUserViewModel vm)
        {
            if(!string.IsNullOrEmpty(vm.Id.ToString()))
            {
                await _userService.UpdateAsync(vm);
                return new OkResult();
               
            }
            else
            {
                var rs = await _userService.AddAsync(vm);
                return new OkObjectResult(rs);
            }
        }
        // delete user by id
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            await _userService.DeleteAsync(id);
            return new OkResult();
        }
    }
}