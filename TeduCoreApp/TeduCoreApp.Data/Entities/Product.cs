﻿using System;
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
        public decimal Original { get; set; }
        //public string Description { get; set; }
        public string Content { get; set; }
        
        public bool? HomeFlag { get; set; }
        public int? ViewCount { get; set; }
        [StringLength(255)]
        public string Tag { get; set; }
        public string Unit { get; set; }

        [ForeignKey("CategoryId")]
        public virtual ProductCategory ProductCategory { set; get; }

        public Status Status { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        [StringLength(255)]
        public string SeoPageTitle { set; get; }
        [Column(TypeName ="varchar")]
        [StringLength(255)]
        public string SeoAlias { set; get; }
        [StringLength(255)]
        public string SeoKeywords { set; get; }
        [StringLength(255)]
        public string Description { set; get; }
    }
}