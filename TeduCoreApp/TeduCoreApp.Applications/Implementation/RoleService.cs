using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.System;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Applications.Implementation
{
    public class RoleService : IRoleService
    {
        private RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        public async Task<List<AppRoleViewModel>> GetAllAsync()
        {
            return await _roleManager.Roles.ProjectTo<AppRoleViewModel>().ToListAsync();
        }
       
    }
}
