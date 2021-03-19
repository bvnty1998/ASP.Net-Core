using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TeduCoreApp.Applications.Implementation;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.Common;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Data.Emuns;
using TeduCoreApp.Utilities.Extensions;

namespace TeduCoreApp.Areas.Admin.Controllers
{
    //[Area("admin")]
    public class BillController : BaseController
    {
        IBillService _billService;
       public BillController(IBillService billService)
        {
            _billService = billService;
        }

        public IActionResult Index()
        {
            return View();
        }
        /// <summary>
        /// Get payment menthod
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetPaymentMenthod()
        {
           
            List<EnumModel> enums = ((PaymentMethod[])Enum.GetValues(typeof(PaymentMethod)))
                .Select(c => new EnumModel()
                {
                    Value = (int)c,
                    Name = c.GetDescription()
                }).ToList();
            return new OkObjectResult(enums);
        }

        /// <summary>
        /// get bill status
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetBillStatus()
        {
            List<EnumModel> enums = ((BillStatus[])Enum.GetValues(typeof(BillStatus)))
                .Select(c => new EnumModel()
                {
                    Value = (int)c,
                    Name = c.GetDescription()
                }).ToList();
            return new OkObjectResult(enums);
        }
        /// <summary>
        /// save bill
        /// </summary>
        /// <param name="billViewModel"></param>
        /// <returns></returns>
        [HttpPost]
        public IActionResult SaveBill(BillViewModel billViewModel)
        {
            _billService.SaveBill(billViewModel);
            return new OkResult();
        }
        /// <summary>
        /// get bill and pagination
        /// </summary>
        /// <param name="keyWord"></param>
        /// <param name="fromDate"></param>
        /// <param name="toDate"></param>
        /// <param name="page"></param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        public IActionResult GetAllPaging(string keyWord,DateTime fromDate,DateTime toDate,int page,int pageSize)
        {
            var dt =_billService.GetAllPaging(keyWord, fromDate, toDate, page, pageSize);
            return new OkObjectResult(dt);
        }

        /// <summary>
        /// find bill by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
       [HttpGet]
        public IActionResult FindBillById(int id)
        {
           var bill = _billService.FindBillById(id);
            return new OkObjectResult(bill);
        }
    }
}