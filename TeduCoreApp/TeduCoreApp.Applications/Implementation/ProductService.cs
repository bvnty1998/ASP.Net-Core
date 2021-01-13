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
using TeduCoreApp.Utilities.ATOs;
using TeduCoreApp.Utilities.Constants;
using TeduCoreApp.Utilities.TextHelper;

namespace TeduCoreApp.Applications.Implementation
{
    public class ProductService : IProductService
    {
        IProductRepository _productRepository;
        ITagRepository _tagRepository;
        IProductTagRepository _productTagRepository;
        IUnitofWork _unitofWork;
        public ProductService(IProductRepository productRepository,ITagRepository tagRepository,
            IProductTagRepository productTagRepository,IUnitofWork unitofWork)
        {
            _productRepository = productRepository;
            _tagRepository = tagRepository;
            _productTagRepository = productTagRepository;
            _unitofWork = unitofWork;
          
        }
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }
        public ProductViewModel Add(ProductViewModel productVm)
        {
           if(!string.IsNullOrEmpty(productVm.Tag))
            {
          
                //Product product = new Product();
                //product = new Product() { Name = "Product tets", DateCreated = DateTime.Now, Image = "/client-side/images/products/product-1.jpg", SeoAlias = "product-test", Price = 1000, Status = Status.Active, OriginalPrice = 1000 ,
                //    CategoryId=6,
                // ProductTags = new List<ProductTag>()
                // {
                //     new ProductTag(){TagId="tag"}
                // }
                //};
                //_productRepository.Add(product);
                Product product = Mapper.Map<ProductViewModel, Product>(productVm);
                List<ProductTag> productTags = new List<ProductTag>();
                string[] arrTag = productVm.Tag.Split(',');
                foreach (var tag in arrTag)
                {
                    var tagId = TextHelper.ToUnsignString(tag);
                    if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                    {
                        Tag t = new Tag
                        {
                            Id = tagId,
                            Name = tag,
                            Type = CommonConstants.ProductTag
                        };
                        _tagRepository.Add(t);
                    }

                    ProductTag producttag = new ProductTag
                    {
                        TagId = tagId
                    };
                    productTags.Add(producttag);

                }
                //ProductTag p = new ProductTag();
                //p.TagId = "tag";
                //p.ProductId = 6;
                //_productTagRepository.Add(p);
                //product.ProductTags.Add(p);
                foreach (var item in productTags)
                {
                    product.ProductTags.Add(item);
                }
                //ProductTag productTag = new ProductTag();
                //productTag.TagId = "Tag";
                //product.ProductTags.Add(productTags = new List<ProductTag>() {
                //    new ProductTag() {TagId = "Tag"}
                //});
                _productRepository.Add(product);
            }
            return productVm;

        }

        public void Delete(int id)
        {
            var product = _productRepository.FindById(id);
            _productRepository.Remove(product);
            _productTagRepository.RemoveMultiple(_productTagRepository.FindAll(x => x.ProductId == product.Id).ToList());
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
            var product = _productRepository.FindById(id);
            return Mapper.Map<Product, ProductViewModel>(product);
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
            //throw new NotImplementedException();
            string[] arrTag = productVm.Tag.Split(',');
            List<ProductTag> productTags = new List<ProductTag>();
            foreach (var tag in arrTag)
            {
                var tagId = TextHelper.ToUnsignString(tag);
                if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                {
                    Tag t = new Tag
                    {
                        Id = tagId,
                        Type = CommonConstants.ProductTag,
                        Name = tag
                    };
                    _tagRepository.Add(t);
                }
                ProductTag productTag = new ProductTag
                {
                    TagId = tagId,
                };
                productTags.Add(productTag);
            }
            var product = Mapper.Map<ProductViewModel, Product>(productVm);
            _productTagRepository.RemoveMultiple(_productTagRepository.FindAll(x => x.ProductId == product.Id).ToList());
            foreach (var item in productTags)
            {
                product.ProductTags.Add(item);
            }
            _productRepository.UpdateProduct(product);
        }

        public void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items)
        {
            throw new NotImplementedException();
        }

        public ProductViewModel UpdateProduct(ProductViewModel productVm)
        {
           
            string[] arrTag = productVm.Tag.Split(',');
            List<ProductTag> productTags = new List<ProductTag>();
            foreach (var tag in arrTag)
            {
                var tagId = TextHelper.ToUnsignString(tag);
                if (!_tagRepository.FindAll(x => x.Id == tagId).Any())
                {
                    Tag t = new Tag
                    {
                        Id = tagId,
                        Type = CommonConstants.ProductTag,
                        Name = tag
                    };
                    _tagRepository.Add(t);
                }
                ProductTag productTag = new ProductTag
                {
                    TagId = tagId,
                };
                productTags.Add(productTag);
            }
            var product = Mapper.Map<ProductViewModel, Product>(productVm);
            _productTagRepository.RemoveMultiple(_productTagRepository.FindAll(x => x.ProductId == product.Id).ToList());
            foreach (var item in productTags)
            {
                product.ProductTags.Add(item);
            }
            _productRepository.UpdateProduct(product);
            return productVm;
        }
    }
}
