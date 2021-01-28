using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Emuns;

namespace TeduCoreApp.Applications.ViewModel.System
{
    public class AppUserViewModel
    {
        public AppUserViewModel()
        {
            Roles = new List<string>();
        }
        public Guid? Id { set; get; }
        public string UserName { set; get; }
        public string PasswordHash { set; get; }
        public string PhoneNumber { set; get; }
        public string Email { set; get; }
        public string FullName { set; get; }
        public DateTime? BirthDate { set; get; }
        public decimal Balace { set; get; }
        public string Avatar { set; get; }
        public Status Status { set; get; }
        public DateTime CreateDate { set; get; }
        public DateTime DateModified { set; get; }
        public List<string> Roles { set; get; }
    }
}
