using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.EF.Extensions;

namespace TeduCoreApp.EF.Configurations
{
    public class FooterCofiguration : DbEntityConfiguration<Footer>
    {
        public override void Configure(EntityTypeBuilder<Footer> entity)
        {
            entity.HasKey(C => C.Id);
            entity.Property(C => C.Id).HasMaxLength(255).HasColumnType("varchar(255)").IsRequired();
        }
    }
}
