using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("AdvertistmentPages")]
    public class AdvertistmentPage :DomainEntity<string>
    {
        public string Name { set; get; }
        public virtual ICollection<AdvertistmentPosition> AdvertistmentPositions { set; get; }
    }
}
