using System;
using System.Collections.Generic;
using System.Text;

namespace TeduCoreApp.Infrastructure.Interfaces
{
   public interface IUnitofWork : IDisposable
    {
        void Commit();
    }
}
