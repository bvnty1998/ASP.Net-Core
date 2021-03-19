using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Applications.ViewModel.System;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Applications.AutoMapper
{
   public class DomainToViewModelMappingProfile:Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>();
            CreateMap<Function, FunctionViewModel>();
            CreateMap<Product, ProductViewModel>();
            CreateMap<AppUser, AppUserViewModel>();
            CreateMap<AppRole, AppRoleViewModel>();
            CreateMap<Permission, PermissionViewModel>();
            CreateMap<Size, SizeViewModel>();
            CreateMap<Color, ColorViewModel>();
            CreateMap<Bill, BillViewModel>();
            CreateMap<BillDetail, BillDetailViewModel>();
        }
    }
}
