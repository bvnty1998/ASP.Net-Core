using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Applications.ViewModel.System
{
   public class PermissionViewModel
    {
        public PermissionViewModel()
        {
            permission = new List<PermissionViewModel>();
        }
        public Guid RoleId { set; get; }
        public string FunctionId { set; get; }
        public bool CanCreate { set; get; }
        public bool CanRead { set; get; }
        public bool CanUpdate { set; get; }
        public bool CanDelete { set; get; }
        List<PermissionViewModel> permission { set; get; }

    }
}
