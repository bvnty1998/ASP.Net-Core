using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Emuns;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("ContactDetails")]
    public class Contact : DomainEntity<string>
    {
        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        [StringLength(50)]
        public string Phone { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        [StringLength(250)]
        public string WebSite { get; set; }
        [StringLength(250)]
        public string Address { get; set; }

        public string Others { get; set; }
        public double? Lat { get; set; }
        public double? Lng { get; set; }
        public Status Status { get; set; }
    }
}
