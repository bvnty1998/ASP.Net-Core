using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Applications.ViewModel.Product;
using TeduCoreApp.Utilities.ATOs;

namespace TeduCoreApp.Applications.Interfaces
{
   public interface IBillService
    {
        void SaveBill(BillViewModel billViewModel);
        PageResult<BillViewModel> GetAllPaging(string keyWord, DateTime fromDate, DateTime toDate, int page, int pageSize);
        BillViewModel FindBillById(int id);
    }

}
