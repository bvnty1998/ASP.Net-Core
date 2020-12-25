using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Emuns;

namespace TeduCoreApp.Applications.ViewModel.System
{
    public class FunctionViewModel
    {
        public string Id { set; get; }
        public string Name
        {
            set; get;
        }
        
        public string Url { set; get; }
       
        public string ParentId { set; get; }
       
        public string IconCss { set; get; }
        
        public int SortOrder { set; get; }
        public Status Status { set; get; }
    }
}
