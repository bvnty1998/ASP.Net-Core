using AutoMapper;
using AutoMapper.QueryableExtensions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.System;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.EF;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Utilities.ATOs;

namespace TeduCoreApp.Applications.Implementation
{

    public class UserService : IUserService
    {
        private readonly UserManager<AppUser> _userManager;
        private readonly AppDbContext db;
        //AppDbContext _appDbContext;
        public UserService(UserManager<AppUser> userManager, AppDbContext context)
        {
            db = context;
            _userManager = userManager;
        }
        public async Task<bool> AddAsync(AppUserViewModel userVM)
        {
            //_appDbContext.ExecuteQuery
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

        public async Task DeleteAsync(string id)
        {

            var param = new SqlParameter("@userId", id);
            var rs = db.Database.ExecuteSqlCommand("tbl_UserAppRoles_Delete @userId", param);
            var user = await _userManager.FindByIdAsync(id);
            var roles = await _userManager.GetRolesAsync(user);
            if(rs == roles.Count())
            {
                await _userManager.DeleteAsync(user);
            }
            
            
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
                
               // kiểm tra roles xem có tồn tại trong roles mới hay ko nếu ko (false) thì xóa
                if (roleVM.Contains(item) == false)
                {
                    object[] Params =
                    {
                        new SqlParameter("userId",userVM.Id),
                        new SqlParameter("nameRole",item)
                    };
                   int res = db.Database.ExecuteSqlCommand("tbl_UserAppRoles_Delete_Single @userId,@nameRole", Params);
                }
                
            }
            // add roles new for user
            var rs = await _userManager.AddToRolesAsync(user, roleVM.Except(roles));
            if(rs.Succeeded)
            {
               
                if (user.PasswordHash != userVM.PasswordHash)
                {
                    //user.PasswordHash = userVM.PasswordHash;
                    var response = await _userManager.RemovePasswordAsync(user);
                    if(response.Succeeded)
                    {
                       var x = await _userManager.AddPasswordAsync(user, userVM.PasswordHash);
                    }
                }
                user.FullName = userVM.FullName;
                user.Email = userVM.Email;
                user.DateModified = DateTime.Now;
                user.PhoneNumber = userVM.PhoneNumber;
                user.Status = userVM.Status;
                // update user
                await _userManager.UpdateAsync(user);
                
            }
            
           
            //throw new NotImplementedException();
        }
    }
}
