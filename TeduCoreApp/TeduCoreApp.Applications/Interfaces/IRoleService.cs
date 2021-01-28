using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Applications.ViewModel.System;
using TeduCoreApp.Utilities.ATOs;

namespace TeduCoreApp.Applications.Interfaces
{
    public interface IRoleService
    {
        Task<List<AppRoleViewModel>> GetAllAsync();
        PageResult<AppRoleViewModel> GetAllPagingAsync(string keyword, int pageCurrent, int pageSize);
        Task<bool> AddRoleAsync(AppRoleViewModel roleVM);
        Task<bool> UpdateRoleAsync(AppRoleViewModel roleVM);
        Task<bool> DeleteRoleAsync(string Id);
        Task<AppRoleViewModel> GetRoleByIdAsync(string Id);
    }
}
