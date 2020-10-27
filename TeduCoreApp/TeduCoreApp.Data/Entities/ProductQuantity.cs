using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ProductQuantities")]
    public class ProductQuantity : DomainEntity<int>
    {
        [Column(Order = 1)]
        public int ProductId { set; get; }
        [Column(Order = 2)]
        public int SizeId { set; get; }
        [Column(Order = 3)]
        public int ColorId { set; get; }
        
        public int Quantity { set; get; }
        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }
        [ForeignKey("SizeId")]
        public virtual Size Size { set; get; }
        [ForeignKey("ColorId")]
        public virtual Color Color { set; get; }
    }
}
