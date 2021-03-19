using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Applications.Interfaces;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class SizeController : BaseController
    {

        ISizeService _sizeService;
        public SizeController(ISizeService sizeService)
        {
            _sizeService = sizeService;
        }
        public IActionResult Index()
        {           
            return View();
        }

        /// <summary>
        /// Get all size
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAll()
        {
            var data = _sizeService.GetAll();
            return new OkObjectResult(data);
        }
    }
}