using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Applications.ViewModel.System;

namespace TeduCoreApp.Applications.Interfaces
{
    public interface IFunctionService :IDisposable
    {
       Task<List<FunctionViewModel>> GetAll();
       List<FunctionViewModel> GetAllByPermission(Guid userId);
       
    }
}
