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
        public Permission()
        {

        }
        public Permission(Guid roleId,string functionId,bool canCreate,bool canRead, bool canUpdate, bool canDelete)
        {
            RoleId = roleId;
            FunctionId = functionId;
            CanCreate = canCreate;
            CanRead = canRead;
            CanUpdate = canUpdate;
            CanDelete = CanDelete;
        }
        
        [Required]
        public Guid RoleId { set; get; }


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
