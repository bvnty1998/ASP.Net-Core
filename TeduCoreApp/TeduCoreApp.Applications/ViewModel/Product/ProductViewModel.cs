using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Emuns;

namespace TeduCoreApp.Applications.ViewModel.Product
{
    public class ProductViewModel
    {
        public int Id { set; get; }
        public string Name { get; set; }

        public int CategoryId { get; set; }


        public string Image { get; set; }

        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }

        public decimal OriginalPrice { get; set; }
        //public string Description { get; set; }
        public string Content { get; set; }

        public bool? HomeFlag { get; set; }
        public int? ViewCount { get; set; }

        public string Tag { get; set; }
        public string Unit { get; set; }


        public ProductCategoryViewModel ProductCategoryViewModel { set; get; }

        public Status Status { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }

        public string SeoPageTitle { set; get; }

        public string SeoAlias { set; get; }

        public string SeoKeywords { set; get; }

        public string Description { set; get; }
    }
}
