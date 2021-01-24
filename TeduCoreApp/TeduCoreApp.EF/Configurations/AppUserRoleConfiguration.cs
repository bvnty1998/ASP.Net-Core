using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.EF.Extensions;

namespace TeduCoreApp.EF.Configurations
{
    public class AppUserRoleConfiguration : DbEntityConfiguration<AppUserRole>
    {
        public override void Configure(EntityTypeBuilder<AppUserRole> entity)
        {
            entity.HasKey(C => C.RoleId);
            entity.HasKey(C => C.UserId);
        }
      
    }
}
