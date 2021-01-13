using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Emuns;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Products")]
   public class Product : DomainEntity<int>, Iswitchable, IDateTracKing, IHasSeoMetaData
    {
        public Product()
        {
            ProductTags = new List<ProductTag>();
        }
        public Product(string name, int categoryid, string image, decimal price, decimal originalPrice,
            decimal promotionPrince, string description, string content, bool? homeFlag,
            string tags, string unit, Status status, string seoPageTitle, string seoAlias, string seoMetaKeyWord)
        {
            Name = name;
            CategoryId = categoryid;
            Image = image;
            Price = price;
            OriginalPrice = originalPrice;
            PromotionPrice = promotionPrince;
            Description = description;
            Content = content;
            HomeFlag = homeFlag;
            Tag = tags;
            Unit = unit;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyWord;
            ProductTags = new List<ProductTag>();
        }
        public Product( int id,string name, int categoryid, string image, decimal price, decimal originalPrice,
            decimal promotionPrince, string description, string content, bool? homeFlag,
            string tags, string unit, Status status, string seoPageTitle, string seoAlias, string seoMetaKeyWord
            , string seoMetaDescription)
        {
            Id = id;
            Name = name;
            CategoryId = categoryid;
            Image = image;
            Price = price;
            OriginalPrice = originalPrice;
            PromotionPrice = promotionPrince;
            Description = description;
            Content = content;
            HomeFlag = homeFlag;
            Tag = tags;
            Unit = unit;
            Status = status;
            SeoPageTitle = seoPageTitle;
            SeoAlias = seoAlias;
            SeoKeywords = seoMetaKeyWord;
            Description = seoMetaDescription;
            ProductTags = new List<ProductTag>();
        }

        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        [Required]
        public int CategoryId { get; set; }
        
        [StringLength(255)]
        public string Image { get; set; }
        [Required]
        [DefaultValue(0)]
        public decimal Price { get; set; }
        public decimal PromotionPrice { get; set; }
        [Required]
        public decimal OriginalPrice { get; set; }
        
        public string Content { get; set; }
        
        public bool? HomeFlag { get; set; }
        public int? ViewCount { get; set; }
        [StringLength(255)]
        public string Tag { get; set; }
        public string Unit { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ProductCategory ProductCategory { set; get; }
        public virtual ICollection<ProductTag> ProductTags { set; get; }

        public Status Status { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        [StringLength(255)]
        public string SeoPageTitle { set; get; }
        [Column(TypeName ="varchar(255)")]
        [StringLength(255)]
        public string SeoAlias { set; get; }
        [StringLength(255)]
        public string SeoKeywords { set; get; }
        [StringLength(255)]
        public string Description { set; get; }
    }
}
