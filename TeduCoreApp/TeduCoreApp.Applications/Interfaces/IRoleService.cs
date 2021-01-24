using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Applications.ViewModel.System;

namespace TeduCoreApp.Applications.Interfaces
{
    public interface IRoleService
    {
        Task<List<AppRoleViewModel>> GetAllAsync();
    }
}
