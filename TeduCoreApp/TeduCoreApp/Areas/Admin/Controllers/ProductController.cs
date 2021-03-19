using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Utilities.TextHelper;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    //[Area("Admin")]
    [Authorize]
    public class ProductController :BaseController
    {
        IProductService _productService;
       
        public ProductController(IProductService productService)
        {
            _productService = productService;
           
        }
        public IActionResult Index()
        {
            return View();
        }

        #region AJAX API
        [HttpGet]
        public IActionResult GetAll()
        {
            var model = _productService.GetAll();
            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetAllPaging(int? categoryId,string keyword,int page,int pageSize )
        {
            var model = _productService.GetAllPaging(categoryId, keyword, page, pageSize);
            return new OkObjectResult(model);
        }

        [HttpGet]
        public IActionResult GetById(int Id)
        {
            var product = _productService.GetById(Id);
            return new OkObjectResult(product);
        }
        /// <summary>
        /// Edit and add product
        /// </summary>
        /// <param name="productViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult Saved(ProductViewModel productViewModel)
        {
            productViewModel.SeoAlias = TextHelper.ToUnsignString(productViewModel.SeoAlias);
            // add product category
            if (productViewModel.Id == 0)
            {
                var product = _productService.Add(productViewModel);
                return new OkObjectResult(product);
            }
            // edit product category
            else
            {
                var product = _productService.UpdateProduct(productViewModel);
               //_productService.UpdateProduct(productViewModel);
                return new OkObjectResult(product);
            }
        }
        ///<summary>
        ///Delete product
        /// </summary>
        /// 
        public IActionResult Delete(int Id )
        {
            _productService.Delete(Id);
            return new OkResult();
        }

        #endregion
    }
}