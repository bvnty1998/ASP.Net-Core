using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Applications.Interfaces;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    public class FucntionController : BaseController
    {
          IFunctionService _functionService; 
        public IActionResult Index(IFunctionService functionService)
        {
            _functionService = functionService;
            return View();
        }

        public IActionResult GetAll()
        {
           var rs = _functionService.GetAllFunction();
            return new OkObjectResult(rs);
        }
    }
}