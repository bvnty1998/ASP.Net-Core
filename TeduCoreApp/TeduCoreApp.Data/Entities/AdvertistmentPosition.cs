using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("AdvertistmentPositions")]
   public class AdvertistmentPosition :DomainEntity<string>
    {
        [StringLength(250)]
        public string PageId { set; get; }
        [StringLength(250)]
        public string Name { set; get; }
        [ForeignKey("PageId")]
        public virtual AdvertistmentPage AdvertistmentPage { set; get; }
       
        public virtual Advertistment Advertistment { set; get; }

    }
}
