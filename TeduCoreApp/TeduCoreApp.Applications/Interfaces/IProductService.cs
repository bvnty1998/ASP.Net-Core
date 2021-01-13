using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Utilities.ATOs;

namespace TeduCoreApp.Applications.Interfaces
{
   public interface IProductService :IDisposable
    {
        ProductViewModel Add(ProductViewModel productVm);

        void Update(ProductViewModel productVm);

        void Delete(int id);

        List<ProductViewModel> GetAll();

        PageResult<ProductViewModel> GetAllPaging(int? categoryId, string keyword, int page, int pageSize);
        List<ProductViewModel> GetAll(string keyword);

        List<ProductViewModel> GetAllByParentId(int parentId);

        ProductViewModel GetById(int id);

        void UpdateParentId(int sourceId, int targetId, Dictionary<int, int> items);
        void ReOrder(int sourceId, int targetId);

        List<ProductViewModel> GetHomeCategories(int top);
        ProductViewModel UpdateProduct(ProductViewModel productVm);



        void Save();
    }
}
