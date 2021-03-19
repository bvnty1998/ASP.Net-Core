using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Applications.ViewModel.Product;

namespace TeduCoreApp.Applications.Interfaces
{
   public interface ISizeService
    {
        List<SizeViewModel> GetAll();
    }
}
