using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Applications.Implementation
{
    public class ColorService : IColorService
    {
        private IColorRepository _colorRepository;
        public ColorService(IColorRepository colorRepository)
        {
            _colorRepository = colorRepository;
        }
        public List<ColorViewModel> GetAll()
        {
            return _colorRepository.FindAll().ProjectTo<ColorViewModel>().ToList();
        }
    }
}
