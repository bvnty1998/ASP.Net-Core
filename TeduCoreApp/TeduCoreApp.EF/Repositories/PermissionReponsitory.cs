using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.EF.Repositories
{
    public class PermissionReponsitory:EFRepository<Permission,int>,IPermissionRepository
    {
        public PermissionReponsitory (AppDbContext context):base(context)
        {
            
        }
    }
}
