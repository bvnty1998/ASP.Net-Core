 using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Data.Emuns;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.Applications.Implementation
{
    public class ProductCategoryService : IProductCategoryService
    {
        IProductCategoryRepository _productCategoryRepository;
        IUnitofWork _unitofWork;
        public ProductCategoryService(IProductCategoryRepository productCategoryRepository,IUnitofWork unitofWork)
        {
            _productCategoryRepository = productCategoryRepository;
            _unitofWork = unitofWork;
        }
        public ProductCategoryViewModel Add(ProductCategoryViewModel productCategoryVm)
        {
            var productCategory = Mapper.Map<ProductCategoryViewModel, ProductCategory>(productCategoryVm);
            _productCategoryRepository.Add(productCategory);
            return productCategoryVm;
        }

        public void Delete(int id)
        {
            _productCategoryRepository.Remove(id);
        }

        public List<ProductCategoryViewModel> GetAll()
        {
          return  _productCategoryRepository.FindAll().OrderBy(x=>x.PerentId).ThenBy(x=>x.SortOrder).
                ProjectTo<ProductCategoryViewModel>().ToList();
        }

        public List<ProductCategoryViewModel> GetAll(string keyword)
        {
            if (!string.IsNullOrEmpty(keyword))
            {
                return _productCategoryRepository.FindAll(x => x.Name.Contains(keyword) || x.Description.Contains(keyword))
                    .OrderBy(x => x.PerentId).ProjectTo<ProductCategoryViewModel>().ToList();
            }
            else
            {
                return _productCategoryRepository.FindAll().OrderBy(x => x.PerentId).
               ProjectTo<ProductCategoryViewModel>().ToList();
            }
        }

        public List<ProductCategoryViewModel> GetAllByParentId(int parentId)
        {
            return _productCategoryRepository.FindAll(x => x.PerentId == parentId && x.Status == Status.Active)
                .ProjectTo<ProductCategoryViewModel>().ToList();
        }

        public ProductCategoryViewModel GetById(int id)
        {
            var dt = _productCategoryRepository.FindById(id);
            return Mapper.Map<ProductCategory, ProductCategoryViewModel>(dt);
        }

        public List<ProductCategoryViewModel> GetHomeCategories(int top)
        {
            var query = _productCategoryRepository.FindAll(x => x.HomeFlag == true
          , c => c.Products).OrderBy(x => x.HomeOrder).Take(top).ProjectTo<ProductCategoryViewModel>();
            var categories = query.ToList();
            return categories;
        }

        public void ReOrder(int sourceId, int targetId)
        {
          var sortOrderOld = _productCategoryRepository.FindById(sourceId);
          var sortOrderNew = _productCategoryRepository.FindById(targetId);
            if(sortOrderOld.PerentId == sortOrderNew.PerentId)
            {
                int temporary = sortOrderOld.SortOrder;
                sortOrderOld.SortOrder = sortOrderNew.SortOrder;
                sortOrderNew.SortOrder = temporary;

                _productCategoryRepository.Update(sortOrderNew);
                _productCategoryRepository.Update(sortOrderOld);
            }

        }

        public void Save()
        {
            _unitofWork.Commit();
        }

        public void Update(ProductCategoryViewModel productCategoryVm)
        {
            var productcategory = Mapper.Map<ProductCategoryViewModel, ProductCategory>(productCategoryVm);
            _productCategoryRepository.Update(productcategory);
            //var category = _productCategoryRepository.FindById(productCategoryVm.Id);
            //category.Name = productCategoryVm.Name;
            //category.DateModified = DateTime.Now;
            //category.Image = productCategoryVm.Image;
            //category.SeoAlias = productCategoryVm.SeoAlias;
            //category.Status = productCategoryVm.Status;
            //category.PerentId = productCategoryVm.PerentId;
            //category.Description = productCategoryVm.Description;
            //category.SortOrder = productCategoryVm.SortOrder;
            //_productCategoryRepository.Update(category);
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            var sortOrderOld = _productCategoryRepository.FindById(sourceId);
            sortOrderOld.PerentId = targetId;
            _productCategoryRepository.Update(sortOrderOld);
        }
    }
}
