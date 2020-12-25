using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Utilities.ATOs
{
    public class PageResult<T> : PageResultBase where T : class
    {
        public PageResult()
        {
            var Result = new List<T>();
        }
        public IList<T> Result{get;set; }
    }
}
