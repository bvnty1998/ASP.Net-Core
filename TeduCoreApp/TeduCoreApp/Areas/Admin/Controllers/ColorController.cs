using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.Product;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class ColorController : BaseController
    {
        IColorService _colorService;
        public ColorController(IColorService colorService)
        {
            _colorService = colorService;
        }
        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Get all color 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _colorService.GetAll();
            return new OkObjectResult(data);
        }
    }
}