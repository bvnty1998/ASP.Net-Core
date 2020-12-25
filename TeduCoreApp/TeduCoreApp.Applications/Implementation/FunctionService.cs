using AutoMapper.QueryableExtensions;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.System;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Applications.Implementation
{
    public class FunctionService : IFunctionService
    {
        IFunctionRepository _functionRepository;
        public FunctionService (IFunctionRepository functionRepository)
        {
            _functionRepository = functionRepository;
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        public Task<List<FunctionViewModel>> GetAll()
        {
            return _functionRepository.FindAll().ProjectTo<FunctionViewModel>().ToListAsync();
        }

        public List<FunctionViewModel> GetAllByPermission(Guid userId)
        {
            throw new NotImplementedException();
        }
    }
}
