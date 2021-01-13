using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    public class ProductTag : DomainEntity<int>
    {
        public ProductTag()
        {
            
        }
        public ProductTag(string tagId)
        {
            TagId = tagId;
        }
        public int ProductId { set; get; }
        [StringLength(50)]
        [Column(TypeName = "varchar(125)")]
        public string TagId { set; get; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }
        
       [ForeignKey("TagId")]
       public virtual Tag Tag { set; get; }
    }
}
