using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Applications.ViewModel.System;

namespace TeduCoreApp.Applications.Interfaces
{
   public interface IPermissionService
    {
        bool AddPermission(List<PermissionViewModel> listPermissionVM);
        List<PermissionViewModel> GetPermissonByRoleId(string roleId);
        List<PermissionViewModel> GetAllPermission();
        bool DeletePermission(string roleId, List<PermissionViewModel> listPermissionVM);
    }
}
