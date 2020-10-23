using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
   public class Tag : DomainEntity<string>
    {
        [MaxLength(255)]
        public string Name { get; set; }
        [MaxLength(255)]
        public string Type { get; set; }
    }
}
