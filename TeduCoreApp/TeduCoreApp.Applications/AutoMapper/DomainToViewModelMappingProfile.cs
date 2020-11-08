using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Applications.AutoMapper
{
   public class DomainToViewModelMappingProfile:Profile
    {
        public DomainToViewModelMappingProfile()
        {
            CreateMap<ProductCategory, ProductCategoryViewModel>();
        }
    }
}
