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

        public List<FunctionViewModel> GetAllFunction()
        {
            var list = _functionRepository.FindAll().ProjectTo<FunctionViewModel>().ToList();
             List<FunctionViewModel> listFunction = new List<FunctionViewModel>();
            for(int i = 0; i < list.Count();i++)
            {
                if(list[i].ParentId == null)
                {
                    listFunction.Add(list[i]);
                }
            }
            for (int i = 0; i < list.Count(); i++)
            {
                if (list[i].ParentId != null)
                {
                    for (int j = 0; j < listFunction.Count(); j++)
                    {
                        if(list[i].ParentId == listFunction[j].Id)
                        {
                            listFunction[j].children.Add(list[i]);
                        }
                    }
                }
            }
            return listFunction;
        }
    }
}
