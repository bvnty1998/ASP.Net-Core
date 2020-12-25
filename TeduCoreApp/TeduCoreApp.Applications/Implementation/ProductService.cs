using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Utilities.ATOs;

namespace TeduCoreApp.Applications.Implementation
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
          
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public ProductViewModel Add(ProductViewModel productVm)
        {
            throw new NotImplementedException();
        }

        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        

        public List<ProductViewModel> GetAll()
        {
            List<Product> product = _productRepository.FindAll(x=>x.ProductCategory).OrderBy(x=>x.DateCreated).ToList();
           return _productRepository.FindAll(x=>x.ProductCategory).OrderBy(x => x.DateCreated).ProjectTo<ProductViewModel>().ToList();

        }
        public PageResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize)
        {
            var query = _productRepository.FindAll(x => x.Status == Data.Emuns.Status.Active);
            if(!string.IsNullOrEmpty(keyword))
            {
                query = query.Where(x => x.Name.Contains(keyword));
            }
            if(categoryId.HasValue)
            {
                query = query.Where(x => x.CategoryId == categoryId.Value);
            }
            int totalRow = query.Count();
            query = query.OrderByDescending(x => x.DateCreated).Skip((page - 1) * pageSize).Take(pageSize);
            var data = query.ProjectTo<ProductViewModel>().ToList();
            var paginationSet = new PageResult<ProductViewModel>()
            {
                Result = data,
                CurrentPage = page,
                RowCount = totalRow,
                PageZise = pageSize

            };
            return paginationSet;

        }


        public List<ProductViewModel> GetAll(string keyword)
        {
            throw new NotImplementedException();
        }

        public List<ProductViewModel> GetAllByParentId(int parentId)
        {
            throw new NotImplementedException();
        }

        public ProductViewModel GetById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ProductViewModel> GetHomeCategories(int top)
        {
            throw new NotImplementedException();
        }

        public void ReOrder(int sourceId, int targetId)
        {
            throw new NotImplementedException();
        }

        public void Save()
        {
            throw new NotImplementedException();
        }

        public void Update(ProductViewModel productVm)
        {
            throw new NotImplementedException();
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            throw new NotImplementedException();
        }

        
    }
}
