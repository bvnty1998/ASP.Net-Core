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
    [Table("SystemConfig")]
    class SystemConfig :DomainEntity<string>,Iswitchable
    {
        [Required]
        [StringLength(255)]
        public string Name { set; get; }
        public string Value1 { set; get; }
        public int? Value2 { set; get; }
        public bool? Value3 { set; get; }
        public DateTime? Value4 { set; get; }
        public Decimal? Value5 { set; get; }
        public Status Status { set; get; }
    }
}
