﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Extensions;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    //[Area("Admin")]
    [Authorize]
    public class HomeController : BaseController
    {
        public IActionResult Index()
        {
            var email = User.GetSpecificClaim("Email");
            var role = User.Claims?.FirstOrDefault(x => x.Type == "Roles").Value;
            return View();
        }
    }
}