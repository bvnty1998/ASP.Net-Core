using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("WholePrices")]
    public class WholePrice :DomainEntity<int>
    {
        public int ProductId { set; get; }
        public int FromQuantity { set; get; }
        public int ToQuantity { set; get; }
        public Decimal Price { set; get; }

        [ForeignKey("ProductId")]
        public virtual Product Product { set; get; }
    }
}
