using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Applications.ViewModel.System;
using TeduCoreApp.Utilities.ATOs;

namespace TeduCoreApp.Applications.Interfaces
{
   public interface IUserService
    {
        Task<bool> AddAsync(AppUserViewModel userVM);
        Task DeleteAsync(string id);
        Task<List<AppUserViewModel>> GetAllAsync();
        PageResult<AppUserViewModel> GetAllPagingAsnync(string keyword, int page, int pageSize);
        Task<AppUserViewModel> GetById(string Id);
        Task UpdateAsync(AppUserViewModel userVM);

    }
}
