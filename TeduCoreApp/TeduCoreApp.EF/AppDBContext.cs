using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.EF.Configurations;
using TeduCoreApp.EF.Extensions;

namespace TeduCoreApp.EF
{
    public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDbContext(DbContextOptions options) : 
            base(options)
        {
        }

     

        public DbSet<Advertistment> Advertistments { set; get; }
        public DbSet<AdvertistmentPage> AdvertistmentPages { set; get; }
        public DbSet<AdvertistmentPosition> AdvertistmentPositions { set; get; }
        public DbSet<Announcement> Announcements { set; get; }
        public DbSet<AnnouncementUser> AnnouncementUsers { set; get; }
        public DbSet<AppRole> AppRoles { set; get; }
        public DbSet<AppUser> AppUsers { set; get; }
        public DbSet<Bill> Bills { set; get; }
        public DbSet<BillDetail> BillDetails { set; get; }
        public DbSet<Blog> Blogs { set; get; }
        public DbSet<BlogTag> BlogTags { set; get; }
        public DbSet<Color> Colors { set; get; }
        public DbSet<Contact> Contacts { set; get; }
        public DbSet<FeedBack> FeedBacks { set; get; }
        public DbSet<Footer> Footers { set; get; }
        public DbSet<Function> Functions { set; get; }
        public DbSet<Language> Languages { set; get; }
        public DbSet<Page> Pages { set; get; }
        public DbSet<Product> Products { set; get; }
        public DbSet<ProductCategory> ProductCategories { set; get; }
        public DbSet<ProductImage> ProductImages { set; get; }
        public DbSet<ProductQuantity> ProductQuantities { set; get; }
        public DbSet<ProductTag> ProductTags { set; get; }
        public DbSet<Size> Sizes { set; get; }
        public DbSet<Slide> Slides { set; get; }
        public DbSet<SystemConfig> SystemConfigs { set; get; }
        public DbSet<Tag> Tags { set; get; }
        public DbSet<Permission> Permissions { set; get; }
        public DbSet<WholePrice> WholePrices { set; get; }
        //public DbSet<AppUserRole> AppUserRoles { set; get; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region
            builder.Entity<IdentityUserClaim<Guid>>().ToTable("AppUserClaims").HasKey(x => x.Id);
            builder.Entity<IdentityRoleClaim<Guid>>().ToTable("AppRoleClaims").HasKey(x => x.Id);
            builder.Entity<IdentityUserLogin<Guid>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            builder.Entity<IdentityUserRole<Guid>>().ToTable("AppUserRoles").HasKey(x => new { x.RoleId, x.UserId });
            builder.Entity<IdentityUserToken<Guid>>().ToTable("AppUserTokens").HasKey(x => new { x.UserId });
            #endregion
            builder.AddConfiguration(new AnnouncementConfiguration());
            builder.AddConfiguration(new AdvertistmentPageCofiguration());
            builder.AddConfiguration(new AdvertistmentPositionConfiguration());
            builder.AddConfiguration(new BlogTagConfiguaration());
            builder.AddConfiguration(new ContactDetailConfiguration());
            builder.AddConfiguration(new FooterCofiguration());
            builder.AddConfiguration(new FunctionCofiguaration());
            builder.AddConfiguration(new PageConfiguaration());
            builder.AddConfiguration(new ProductTagCofiguaration());
            builder.AddConfiguration(new TagConfiguration());
            //builder.AddConfiguration(new AppUserRoleConfiguration());
            //base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach (EntityEntry item in modified)
            {
                var changedOrAddItem = item.Entity as IDateTracKing;
                if (changedOrAddItem != null)
                {
                    if (item.State == EntityState.Added)
                    {
                        changedOrAddItem.DateCreated = DateTime.Now;
                    }
                    else
                    {
                        changedOrAddItem.DateModified = DateTime.Now;
                    }
                }
            }
            return base.SaveChanges();
        }
    }

    public class DesignTimeBbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
    {
        public AppDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuaration = new ConfigurationBuilder()
                 .SetBasePath(Directory.GetCurrentDirectory())
                 .AddJsonFile("appsettings.json").Build();
            var builder = new DbContextOptionsBuilder<AppDbContext>();
            var conectionString = configuaration.GetConnectionString("DefaultConnection");
            builder.UseSqlServer(conectionString);
            return new AppDbContext(builder.Options);
        }
    }
}
