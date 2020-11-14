using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.EF.Extensions;

namespace TeduCoreApp.EF.Configurations
{
    public class AdvertistmentPageCofiguration : DbEntityConfiguration<AdvertistmentPage>
    {
        public override void Configure(EntityTypeBuilder<AdvertistmentPage> entity)
        {
      
            entity.Property(c => c.Id).IsRequired().HasColumnType("varchar(250)");
            
        }
    }
}
