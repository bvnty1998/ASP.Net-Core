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
    public class RoleService : IRoleService
    {
        private RoleManager<AppRole> _roleManager;
        public RoleService(RoleManager<AppRole> roleManager)
        {
            _roleManager = roleManager;
        }
        /// <summary>
        /// Get all role
        /// </summary>
        /// <returns></returns>
        public async Task<List<AppRoleViewModel>> GetAllAsync()
        {
            return await _roleManager.Roles.ProjectTo<AppRoleViewModel>().ToListAsync();
        }

        /// <summary>
        /// Get all role and paging 
        /// </summary>
        /// <param name="keyword"></param>
        /// <param name="pageCurrent"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
       public PageResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int pageCurrent, int pageSize)
        {
            // câu query lấy tât cả role có trong database
            var query = _roleManager.Roles;
            // nếu keyword khác null câu thì thêm điều kiện vào câu query
            if (!String.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }
            // lây số lượng row trả về
            var totalRow = query.Count();
            // câu query thêm điện lấy số lượng bản ghi theo pageCurrent và pageSize truyền vào
            query = query.Skip((pageCurrent - 1) * pageSize).Take(pageSize);
            // lúc này câu query mới được thực thi bằng lệnh .Tolist()
            var data = query.ProjectTo<AppRoleViewModel>().ToList();
            var paginationSet = new PageResult<AppRoleViewModel>()
            {
                Result = data,
                PageZise = pageSize,
                CurrentPage = pageCurrent,
                RowCount = totalRow
            };
            return paginationSet;
        }
        /// <summary>
        /// Add role
        /// </summary>
        /// <param name="roleVM"></param>
        /// <returns></returns>
        public async Task<bool> AddRoleAsync(AppRoleViewModel roleVM)
        {
            var role = new AppRole()
            {
                Name = roleVM.Name,
                Description = roleVM.Description
            };
            var rs = await _roleManager.CreateAsync(role);
            if (rs.Succeeded == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        /// <summary>
        /// Update role
        /// </summary>
        /// <param name="roleVM"></param>
        /// <returns></returns>
        public async Task<bool> UpdateRoleAsync(AppRoleViewModel roleVM)
        {
            var role = await _roleManager.FindByIdAsync(roleVM.id.ToString());
            role.Description = roleVM.Description;
            var rs = await _roleManager.UpdateAsync(role);
            if (rs.Succeeded == true)
            {
                return true;
            }
            else
            {
                return false;
            }
            
        }
        /// <summary>
        /// delete role by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<bool> DeleteRoleAsync(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            var rs = await _roleManager.DeleteAsync(role);
            if(rs.Succeeded == true)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// Find Role by id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        public async Task<AppRoleViewModel> GetRoleByIdAsync(string Id)
        {
            var role = await _roleManager.FindByIdAsync(Id);
            var rs = Mapper.Map<AppRole,AppRoleViewModel>(role);
            return rs;
        }
    }
}
