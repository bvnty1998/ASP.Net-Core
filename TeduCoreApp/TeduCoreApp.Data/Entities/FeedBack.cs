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
    [Table("FeeBacks")]
    public class FeedBack : DomainEntity<int>, Iswitchable, IDateTracKing
    {
        [StringLength(255)]
        [Required]
        public string Name { get; set; }
        [StringLength(250)]
        public string Email { get; set; }
        [StringLength(500)]
        public string Message { get; set; }
        public Status Status { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
    }
}
