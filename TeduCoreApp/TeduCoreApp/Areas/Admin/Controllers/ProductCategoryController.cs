using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.Product;

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
        #endregion
    }
}