using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.System;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Utilities.ATOs;

namespace TeduCoreApp.Applications.Implementation
{

    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
       
        public UserService(UserManager<AppUser> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> AddAsync(AppUserViewModel userVM)
        {
            AppUser user = new AppUser()
            {
                FullName = userVM.FullName,
                UserName = userVM.UserName,
                Email = userVM.Email,
                PhoneNumber = userVM.PhoneNumber,
                DateCreated = DateTime.Now,
                Status = userVM.Status

            };
            var rs = await _userManager.CreateAsync(user, userVM.PasswordHash);
            var listRoles = userVM.Roles.ToList();
            if(rs.Succeeded)
            {
                foreach(var item in listRoles)
                {
                   var a = await _userManager.AddToRoleAsync(user, item);
                }
                return   true;
            }
            else
            {
                return false;
            }
            
        }

        public Task DeleteAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<AppUserViewModel>> GetAllAsync()
        {
           
            return await _userManager.Users.ProjectTo<AppUserViewModel>().ToListAsync();
        }

        public  PageResult<AppUserViewModel> GetAllPagingAsnync(string keyword, int page, int pageSize)
        {
            var query = _userManager.Users;
           
            if(!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.UserName.Contains(keyword));
            }
            query = query.Skip((page - 1) * pageSize).Take(pageSize);
            var data = query.ProjectTo<AppUserViewModel>().ToList();
            var totalRow = query.Count();
            var paginationSet = new PageResult<AppUserViewModel>()
            {
                Result = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageZise = pageSize
            };
            return  paginationSet;
            
        }

        public async Task<AppUserViewModel> GetById(string Id)
        {
            var user =await _userManager.FindByIdAsync(Id);
            var roles =await _userManager.GetRolesAsync(user);
            var userVm = Mapper.Map<AppUser, AppUserViewModel>(user);
            userVm.Roles = roles.ToList();
            return  userVm;

        }

        public async Task UpdateAsync(AppUserViewModel userVM)
        {
            var user = await _userManager.FindByIdAsync(userVM.Id.ToString());
            //AppUserRole appUserRole = new AppUserRole();
            
            var roles = await _userManager.GetRolesAsync(user);
            var roleVM = userVM.Roles.ToList();
            foreach (var item in roles)
            {
                var s = await _userManager.GetUsersInRoleAsync(item);
               
                if (roleVM.Contains(item) == false)
                {
                   
                    // delete roles 
                    
                    var c = await _userManager.RemoveFromRoleAsync(user, item.ToString());
                    var f = await _userManager.RemoveFromRoleAsync(user, "STAFF");
                }
                
            }
            // add roles new for user
            var rs = await _userManager.AddToRolesAsync(user, roleVM.Except(roles));
            if(rs.Succeeded)
            {
                user.FullName = userVM.FullName;
                user.Email = userVM.Email;
                user.DateModified = DateTime.Now;
                user.PhoneNumber = user.PhoneNumber;
                if (user.PasswordHash != userVM.PasswordHash)
                {
                    user.PasswordHash = userVM.PasswordHash;
                }
                // update user
                await _userManager.UpdateAsync(user);
                
            }
            
           
            //throw new NotImplementedException();
        }
    }
}
