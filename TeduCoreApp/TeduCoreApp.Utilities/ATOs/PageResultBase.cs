using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Utilities.ATOs
{
    public abstract class PageResultBase
    {
        public int CurrentPage { set; get; }

        public int PageCount
        {
            get
            {
                var PageCount = (double)RowCount / PageZise;
                return (int)Math.Ceiling(PageCount);
            }
            set
            {
                PageCount = value;
            }
        }
        public int PageZise{ set; get;}

        public int RowCount { set; get; }

        public int RowFistPage
        {
            get
            {
                return (CurrentPage - 1) * PageZise;
            }
        }

        public int RowLastPage
        {
            get
            {
                return Math.Min(CurrentPage * PageZise, RowCount);
            }
        }
        

    }
}
