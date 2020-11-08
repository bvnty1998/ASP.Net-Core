using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.EF.Configurations;
using TeduCoreApp.EF.Extensions;

namespace TeduCoreApp.EF
{
    public class AppDBContext : IdentityDbContext<AppUser, AppRole, Guid>
    {
        public AppDBContext(DbContextOptions options) : base(options)
        {
        }
        public DbSet<Advertistment> Advertistments { set; get; }
        public DbSet<AdvertistmentPage> AdvertistmentPages { set; get; }
        public DbSet<AdvertistmentPosition> GetAdvertistmentPositions { set; get; }
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
        public DbSet<WholePrice> WholePrices { set; get; }


        protected override void OnModelCreating(ModelBuilder builder)
        {
            #region
            builder.Entity<IdentityUserClaim<string>>().ToTable("AppUserClaims").HasKey(x => x.Id);
            builder.Entity<IdentityRoleClaim<string>>().ToTable("AppRoleClaims").HasKey(x => x.Id);
            builder.Entity<IdentityUserLogin<string>>().ToTable("AppUserLogins").HasKey(x => x.UserId);
            builder.Entity<IdentityUserRole<string>>().ToTable("AppUserRoles").HasKey(x => new { x.RoleId,x.UserId});
            builder.Entity<IdentityUserToken<string>>().ToTable("AppUserTokens").HasKey(x => new { x.UserId});
            #endregion

            builder.AddConfiguration(new AdvertistmentPositionConfiguration());
            builder.AddConfiguration(new BlogTagConfiguaration());
            builder.AddConfiguration(new ContactDetailConfiguration());
            builder.AddConfiguration(new FooterCofiguration());
            builder.AddConfiguration(new FunctionCofiguaration());
            builder.AddConfiguration(new PageConfiguaration());
            builder.AddConfiguration(new ProductTagCofiguaration());
            builder.AddConfiguration(new TagConfiguration());
            base.OnModelCreating(builder);
        }
        public override int SaveChanges()
        {
            var modified = ChangeTracker.Entries().Where(e => e.State == EntityState.Modified || e.State == EntityState.Added);
            foreach(EntityEntry item in modified)
            {
                var changedOrAddItem = item.Entity as IDateTracKing;
                if(changedOrAddItem != null)
                {
                    if(item.State == EntityState.Added)
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
}
