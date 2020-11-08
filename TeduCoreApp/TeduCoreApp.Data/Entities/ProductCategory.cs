using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Emuns;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ProductCategories")]
  public  class ProductCategory : DomainEntity<int>, IHasSeoMetaData, Iswitchable, ISortable, IDateTracKing
    {
        public ProductCategory()
        {
            Products = new List<Product>();
        }
        public ProductCategory(string name,int? parentId,int? homeOrder, string image,bool? homeFlag,string seoPageTital,
            string seoAlias,string seoKeyWords,string description ,Status status, int sortOrder,DateTime dateCreated,
            DateTime dateModified)
        {
            Name = name;
            PerentId = parentId;
            HomeOrder = homeOrder;
            Image = image;
            HomeFlag = homeFlag;
            SeoPageTitle = seoPageTital;
            SeoAlias = seoAlias;
            SeoKeywords = seoKeyWords;
            Description = description;
            Status = status;
            SortOrder = sortOrder;
            DateCreated = dateCreated;
            DateModified = dateModified;
        }
        public string Name { get; set; }
        
        public int? PerentId { get; set; }
        public int? HomeOrder { get; set; }
        public string Image { get; set; }
        public bool? HomeFlag { get; set; }
        public string SeoPageTitle { get; set; }
        public string SeoAlias { get; set; }
        public string SeoKeywords { get; set; }
        public string Description { get; set; }
        public Status Status { get; set; }
        public int SortOrder { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }

        public virtual ICollection<Product> Products { set; get; }
    }
}
