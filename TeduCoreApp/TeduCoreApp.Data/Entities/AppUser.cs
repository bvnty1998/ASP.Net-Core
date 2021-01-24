using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using TeduCoreApp.Data.Emuns;
using TeduCoreApp.Data.Interfaces;

namespace TeduCoreApp.Data.Entities
{
    [Table("AppUsers")]
    public class AppUser : IdentityUser<Guid>, IDateTracKing, Iswitchable
    {
        public AppUser()
        {

        }

        //public AppUser(string fullName, string username, string password, string phone, string email, DateTime? birthday
        //    , decimal balance, string avatar, Status status, DateTime datecreate)
        public AppUser(Guid? id,string fullName,DateTime? birthday
            ,decimal balance,string avatar,Status status,DateTime datecreate )
        {
            FullName = fullName;
            BirthDay = birthday;
            Balance = balance;
            Avatar = avatar;
            Status = Status;
            DateCreated = datecreate;


        }
        public string FullName { set; get; }
        public DateTime? BirthDay {set;get;}
        public decimal Balance { set; get; }
        public string Avatar { set; get; }
        public Status Status { set; get; }
        public DateTime DateCreated { set; get; }
        public DateTime DateModified { set; get; }
    }
}
