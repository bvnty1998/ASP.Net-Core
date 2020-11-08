using AutoMapper;
using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Data.Entities;

namespace TeduCoreApp.Applications.AutoMapper
{
   public class ViewModelToDomainMappingProfile :Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ProductCategoryViewModel, ProductCategory>()
                .ConstructUsing(c => new ProductCategory(c.Name,c.PerentId,c.HomeOrder,c.Image,c.HomeFlag,c.SeoPageTitle
                ,c.SeoAlias,c.SeoKeywords,c.Description,c.Status,c.SortOrder,c.DateCreated,c.DateModified));
        }
    }
}
