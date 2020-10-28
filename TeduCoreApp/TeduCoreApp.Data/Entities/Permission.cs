using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.Data.Entities
{
    [Table("Permissions")]
    public class Permission : DomainEntity<int>
    {
        [StringLength(450)]
        [Required]
        public string RoleId { set; get; }


        [StringLength(128)]
        [Required]
        public string FunctionId { set; get; }

        public bool CanCreate { set; get; }
        public bool CanRead { set; get; }
        public bool CanUpdate { set; get; }
        public bool CanDelete { set; get; }

        [ForeignKey("RoleId")]
        public virtual AppRole AppRole { set; get; }
        [ForeignKey("FunctionId")]
        public virtual Function Function { set; get; }

    }
}
