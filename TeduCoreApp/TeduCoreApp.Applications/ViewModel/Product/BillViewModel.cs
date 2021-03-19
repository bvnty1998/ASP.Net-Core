using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Applications.ViewModel.System;
using TeduCoreApp.Data.Emuns;

namespace TeduCoreApp.Applications.ViewModel.Product
{
    public class BillViewModel 
    {
        public int Id { set; get; }
        public string CustomerName { set; get; }
        public string CustomerAddress { set; get; }
        public string CustomerMobile { set; get; }
        public string CustomerMessage { set; get; }
        public PaymentMethod PaymentMethod { set; get; }
        public BillStatus BillStatus { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
        public Status Status { set; get; } = Status.Active;
        public Guid? CustomerId { set; get; }
        public  AppUserViewModel User { set; get; }

        public List<BillDetailViewModel> BillDetailViewModel { set; get; }
        public ICollection<BillDetailViewModel> BillDetails { set; get; }

    }
}
