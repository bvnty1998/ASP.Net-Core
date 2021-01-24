using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;
using Microsoft.EntityFrameworkCore;
namespace TeduCoreApp.EF.Repositories
{
    public class AppUserRoleRepository : IAppUserRoleRepository
    {
        private readonly AppDbContext _context;

        public AppUserRoleRepository(AppDbContext context)
        {
            _context = context;
        }
        public void Add(AppUserRole entity)
        {
            _context.Add(entity);
            _context.SaveChanges();
        }
        public void Dispose()
        {
            if (_context != null)
            {
                _context.Dispose();
            }
        }
        public IQueryable<AppUserRole> FindAll(params Expression<Func<AppUserRole, object>>[] includeProperties)
        {
            IQueryable<AppUserRole> items = _context.Set<AppUserRole>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        public IQueryable<AppUserRole> FindAll(Expression<Func<AppUserRole, bool>> predicate, params Expression<Func<AppUserRole, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public AppUserRole FindById(Guid id, params Expression<Func<AppUserRole, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public AppUserRole FindSingle(Expression<Func<AppUserRole, bool>> predicate, params Expression<Func<AppUserRole, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(AppUserRole entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void RemoveMultiple(List<AppUserRole> enities)
        {
            throw new NotImplementedException();
        }

        public void Update(AppUserRole entity)
        {
            throw new NotImplementedException();
        }
    }
}
