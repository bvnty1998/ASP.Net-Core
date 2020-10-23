using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Data.Interfaces
{
   public interface IDateTracKing
    {
        DateTime DateCreated { set; get; }
        DateTime DateModified { get; set; }
    }
}
