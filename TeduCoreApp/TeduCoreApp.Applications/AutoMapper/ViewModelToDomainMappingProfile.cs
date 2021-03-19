using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Applications.ViewModel.System;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Applications.AutoMapper
{
   public class ViewModelToDomainMappingProfile :Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductCategoryViewModel, ProductCategory>()
                .ConstructUsing(c => new ProductCategory(c.Name, c.PerentId, c.HomeOrder, c.Image, c.HomeFlag, c.SeoPageTitle
                , c.SeoAlias, c.SeoKeywords, c.Description, c.Status, c.SortOrder, c.DateCreated, c.DateModified));
            CreateMap<ProductViewModel, Product>().ConstructUsing(c => new Product(c.Name, c.CategoryId, c.Image, c.Price, c.OriginalPrice, c.PromotionPrice
                , c.Description, c.Content, c.HomeFlag, c.Tag, c.Unit, c.Status, c.SeoPageTitle, c.SeoAlias, c.SeoKeywords));
            CreateMap<AppUserViewModel, AppUser>().ConstructUsing(c => new AppUser(c.Id, c.FullName, c.BirthDate, c.Balace, c.Avatar, c.Status, c.CreateDate));
            CreateMap<PermissionViewModel, Permission>().ConstructUsing(c => new Permission(c.RoleId, c.FunctionId, c.CanCreate, c.CanRead,
                 c.CanUpdate, c.CanDelete));
            CreateMap<BillViewModel, Bill>().ConstructUsing(c => new Bill(c.CustomerName, c.CustomerAddress, c.CustomerMobile, c.CustomerMessage,
                 c.BillStatus, c.PaymentMethod, c.Status, c.CustomerId));
        }
    }
}
