using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.EF.Repositories
{
    public class FuctionReponsitory : EFRepository<Function,string>, IFunctionRepository
    {
        public FuctionReponsitory(AppDbContext context) :base(context)
        {

        }
    }
}
