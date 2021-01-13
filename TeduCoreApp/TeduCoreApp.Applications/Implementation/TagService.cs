using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using TeduCoreApp.Data.Entities;
using TeduCoreApp.Data.IRepositories;

namespace TeduCoreApp.Applications.Implementation
{
    public class TagService : ITagRepository
    {
        public void Add(Tag entity)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tag> FindAll(params Expression<Func<Tag, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public IQueryable<Tag> FindAll(Expression<Func<Tag, bool>> predicate, params Expression<Func<Tag, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Tag FindById(string id, params Expression<Func<Tag, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public Tag FindSingle(Expression<Func<Tag, bool>> predicate, params Expression<Func<Tag, object>>[] includeProperties)
        {
            throw new NotImplementedException();
        }

        public void Remove(Tag entity)
        {
            throw new NotImplementedException();
        }

        public void Remove(string Id)
        {
            throw new NotImplementedException();
        }

        public void RemoveMultiple(List<Tag> enities)
        {
            throw new NotImplementedException();
        }

        public void Update(Tag entity)
        {
            throw new NotImplementedException();
        }
    }
}
