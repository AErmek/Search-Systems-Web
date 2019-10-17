using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Searcher.DAL.Interfaces
{
    public interface IGenericRepository<T> where T : class
    {
        IQueryable<T> Find(Expression<Func<T, bool>> predicate);
        Task<T> GetAsync(int id);
        Task AddAsync(T entity);
        Task AddRangeAsync(IEnumerable<T> entities);
        void Remove(T entity);
        T Edit(T entity);
    }
}
