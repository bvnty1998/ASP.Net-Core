using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.Product;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    //[Area("Admin")]
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
        #endregion
    }
}