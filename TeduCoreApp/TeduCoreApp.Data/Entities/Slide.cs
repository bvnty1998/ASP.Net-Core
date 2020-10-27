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
    [Table("Slides")]
    public class Slide:DomainEntity<int>,Iswitchable
    {
        [StringLength(255)]
        [Required]
        public string Name { set; get; }
        [StringLength(255)]
        public string Description { set; get; }
        [StringLength(255)]
        [Required]
        public string Image { set; get; }
        [StringLength(255)]
        public string Url { set; get; }
        [StringLength(255)]
        [Required]
        public int? DisplayOrder { set; get; }
        public Status Status { set; get; }
        public string Content { get; set; }
        [StringLength(15)]
        [Required]
        public string GroupAlias { set; get; }
    }
}
