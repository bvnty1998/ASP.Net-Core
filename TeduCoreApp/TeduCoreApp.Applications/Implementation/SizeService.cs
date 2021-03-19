using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Applications.Implementation
{
    public class SizeService : ISizeService
    {
        ISizeRepository _sizeRepository;
        public SizeService(ISizeRepository sizeRepository)
        {
            _sizeRepository = sizeRepository;
        }
        public List<SizeViewModel> GetAll()
        {
            return  _sizeRepository.FindAll().ProjectTo<SizeViewModel>().ToList();
        }
    }
}
