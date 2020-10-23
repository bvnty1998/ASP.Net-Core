using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Emuns;

namespace TeduCoreApp.Data.Interfaces
{
   public interface Iswitchable
    {
        Status Status { set; get; }
    }
}
