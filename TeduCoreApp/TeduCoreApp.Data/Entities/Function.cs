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
    [Table("Fuctions")]
   public class Function : DomainEntity<string>, Iswitchable, ISortable
    {
        public Function()
        {

        }
        public Function(string name, string parentId, int sortOrder, Status status, string url, string iconCss)
        {
            this.Name = name;
            this.Url = url;
            this.ParentId = parentId;
            this.IconCss = iconCss;
            this.SortOrder = sortOrder;
            this.Status = Status.Active;

        }
        [StringLength(250)]
        [Required]
        public string Name
        {
            set; get;
        }
        [StringLength(250)]
        public string Url { set; get; }
        [StringLength(250)]
        public string ParentId { set; get; }
        [StringLength(250)]
        public string IconCss { set; get; }
        [StringLength(250)]
        public int SortOrder { set; get; }
        public Status Status { set; get; }

    }
}
