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
    [Table("Pages")]
    public class Page : DomainEntity<int>, Iswitchable
    {
        [MaxLength(255)]
        [Required]
        public int Name { get; set; }
        [MaxLength(255)]
        [Required]
        public int Alias { get; set; }
        public int Content { get; set; }
        public Status Status { set; get; }
    }
}
