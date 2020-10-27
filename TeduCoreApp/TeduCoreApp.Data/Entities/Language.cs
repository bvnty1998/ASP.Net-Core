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
    [Table("Languages")]
    public class Language : DomainEntity<string>, Iswitchable
    {
        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        public string IsDefault { get; set; }
        public string Resource { get; set; }
        public Status Status { set; get; }
    }
}
