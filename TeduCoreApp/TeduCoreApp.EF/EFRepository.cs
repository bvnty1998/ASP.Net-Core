using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TeduCoreApp.Infrastructure.Interfaces;
using TeduCoreApp.Infrastructure.SharedKernel;

namespace TeduCoreApp.EF
{
    public class EFRepository<T, K> : IRepository<T, K>, IDisposable where T : DomainEntity<K>
    {
        private readonly AppDBContext _context;
        public EFRepository(AppDBContext context)
        {
            _context = context;
        }
        public void Add(T entity)
        {
            _context.Add(entity);
        }

        public void Dispose()
        {
            if(_context != null)
            {
                _context.Dispose();
            }
        }

        public IQueryable<T> FindAll(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if(includeProperties !=null)
            {
                foreach(var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items;
        }

        public IQueryable<T> FindAll(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> items = _context.Set<T>();
            if (includeProperties != null)
            {
                foreach (var includeProperty in includeProperties)
                {
                    items = items.Include(includeProperty);
                }
            }
            return items.Where(predicate);
        }

        public T FindById(K id, params Expression<Func<T, object>>[] includeProperties)
        {
            //Select Top 1 * from tblT where id=@id and Name=@name and Age=30
            //includeProperties[0] >> ĐK id=@id
            //includeProperties[1] >> ĐK Name=@Name
            //includeProperties[2] >> ĐK And=@age
            return FindAll(includeProperties).SingleOrDefault(x=>x.Id.Equals(id));
        }

        public T FindSingle(Expression<Func<T, bool>> predicate, params Expression<Func<T, object>>[] includeProperties)
        {

            //B1: Có tập danh sách nhân viên
            //B2: Tìm nhân viên nào theo [ID or Name or ĐK bất nào đó]
            // predicate1=DanhSach.select(item =>item.Name="A.A")
            // predicate2=DanhSach.select(item =>item.Age=30)
            return FindAll(includeProperties).SingleOrDefault(predicate);
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void Remove(K Id)
        {
            Remove(FindById(Id));
        }

       

        public void RemoveMultiple(List<T> enities)
        {
            _context.RemoveRange(enities);
        }

        public void Update(T entity)
        {
            _context.Update(entity);
        }
    }
}
