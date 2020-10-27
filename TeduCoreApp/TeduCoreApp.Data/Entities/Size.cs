using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Sizes")]
    public class Size: DomainEntity<int>
    {
        [StringLength(255)]
        public string Name { set; get; }
    }
}
