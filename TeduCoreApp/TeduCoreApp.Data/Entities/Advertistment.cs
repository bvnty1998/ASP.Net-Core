using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Emuns;
using TeduCoreApp.Data.Interfaces;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Advertistments")]
   public class Advertistment :DomainEntity<int>,Iswitchable,ISortable
    {
        [StringLength(250)]
        public string Name { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        [StringLength(250)]
        public string Image { get; set; }

        [StringLength(250)]
        public string Url { get; set; }
        [StringLength(20)]
        public string PositionId { get; set; }

        public Status Status { get; set; }
        public int SortOrder { get; set; }
        [ForeignKey("PositionId")]
        public virtual AdvertistmentPosition AdvertistmentPosition { set; get; }
    }
}
