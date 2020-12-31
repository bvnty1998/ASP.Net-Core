using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Utilities.TextHelper;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class ProductCategoryController : BaseController
    {
        IProductCategoryService _productCategoryService;
        public ProductCategoryController(IProductCategoryService productCategoryService)
        {
            _productCategoryService = productCategoryService;
        }
        public IActionResult Index()
        {
            return View();
        }
        #region AJAX API
        public IActionResult GetAllCategory()
        {
             List<ProductCategoryViewModel> listProduct = _productCategoryService.GetAll();
            return new  OkObjectResult(listProduct);
        }

        [HttpPost]
        public IActionResult ReOrder( int sourceId ,int targetNodeId)
        {
            if (!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                if(sourceId == targetNodeId)
                {
                    return new BadRequestResult();
                }
                else
                {
                    _productCategoryService.ReOrder(sourceId, targetNodeId);
                    return new OkResult();
                }
            }
        }
        [HttpPost]
        public IActionResult UpdateParentId (int sourceId, int targetId, Dictionary<int, int> items)
        {
            if(!ModelState.IsValid)
            {
                return new BadRequestObjectResult(ModelState);
            }
            else
            {
                _productCategoryService.UpdateParentId(sourceId, targetId, items);
                return new OkResult();
            }
        }

        [HttpGet]
        public IActionResult GetById (int id)
        {
           var productCategory = _productCategoryService.GetById(id);
            return new OkObjectResult(productCategory);
        }
        /// <summary>
        /// Update and create product category
        /// </summary>
        /// <param name="productCategoryViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Saved(ProductCategoryViewModel productCategoryViewModel)
        {
           if(!ModelState.IsValid)
            {
                IEnumerable<ModelError> modelErrors = ModelState.Values.SelectMany(x => x.Errors);
                return new BadRequestObjectResult(ModelState);
            }
           else
            {
                productCategoryViewModel.SeoAlias = TextHelper.ToUnsignString(productCategoryViewModel.SeoAlias);
                if (productCategoryViewModel.Id == 0)
                {
                    var category = _productCategoryService.Add(productCategoryViewModel);
                    return new OkObjectResult(category);
                }
                else
                {
                    _productCategoryService.Update(productCategoryViewModel);
                    return new OkResult();
                }
            }
        }
        ///<summary>
        /// delete product category
        /// </summary>
        /// 
        public IActionResult Delete(int Id)
        {
            _productCategoryService.Delete(Id);
            return new OkResult();
        }
        #endregion
    }
}