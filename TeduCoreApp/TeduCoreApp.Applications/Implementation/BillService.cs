using AutoMapper;
using AutoMapper.QueryableExtensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TeduCoreApp.Applications.Interfaces;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using TeduCoreApp.Utilities.ATOs;

namespace TeduCoreApp.Applications.Implementation
{
    public class BillService : IBillService
    {
        IBillRepository _billRepository;
        public BillService(IBillRepository billRepository)
        {
            _billRepository = billRepository;
        }

        public BillViewModel FindBillById(int id)
        {
            var bill = _billRepository.FindAll(x=>x.Id == id).ProjectTo<BillViewModel>().SingleOrDefault();
            return bill;
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
        public PageResult<BillViewModel> GetAllPaging(string keyWord, DateTime fromDate, DateTime toDate, int page, int pageSize)
        {
            var query = _billRepository.FindAll();
            // filter by keyword
            if(!string.IsNullOrEmpty(keyWord))
            {
               query= query.Where(x => x.CustomerName.Contains(keyWord));
            }
            // filter by fromDate and toDate
            if(fromDate != DateTime.Parse("01/01/0001") && toDate != DateTime.Parse("01/01/0001"))
            {
              query= query.Where(x => x.DateCreated >= fromDate && x.DateCreated <= toDate);
            }
            // count total record
            var totalRecord = query.Count();
            // get record by page index current
            query = query.OrderBy(x => x.DateCreated).Skip((page - 1) * pageSize).Take(pageSize);
            var data = query.ProjectTo<BillViewModel>(query).ToList();
            var paginationSet = new PageResult<BillViewModel>()
            {
                Result = data,
                RowCount = totalRecord,
                CurrentPage = page,
                PageZise = pageSize

            };
            return paginationSet;
           
        }

        /// <summary>
        /// Save a new bill or update bill 
        /// </summary>
        /// <param name="billViewModel"></param>
        public void SaveBill(BillViewModel billViewModel)
        {
            Bill bill = Mapper.Map<BillViewModel, Bill>(billViewModel);
            List<BillDetail> BillDetails = new List<BillDetail>();
            foreach(var item in billViewModel.BillDetailViewModel)
            {
                var BillDetail = new BillDetail()
                {
                    ColorId = item.ColorId,
                    Price = item.Price,
                    SizeId = item.SizeId,
                    Quantity = item.Quantity,
                    ProductId = item.ProductId
                };
                BillDetails.Add(BillDetail);
            }
            bill.BillDetails = BillDetails;
            _billRepository.Add(bill);
        }
    }
}
