using System;
using System.Collections.Generic;
using System.Text;
using TeduCoreApp.Infrastructure.Interfaces;

namespace TeduCoreApp.EF
{
    public class EFUnitofWork : IUnitofWork
    {
        private readonly AppDBContext _context;
        public EFUnitofWork (AppDBContext context)
        {
            _context = context;
        }
        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
