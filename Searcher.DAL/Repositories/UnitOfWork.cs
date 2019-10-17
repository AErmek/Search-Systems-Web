using Microsoft.EntityFrameworkCore;
using Searcher.DAL.EF;
using Searcher.DAL.Interfaces;
using System;
using System.Collections.Concurrent;
using System.Threading.Tasks;

namespace Searcher.DAL.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly SearcherContext _context;
        private readonly ConcurrentDictionary<Type, object> _repositories;
        private bool _disposed;


        public DbContext Context => _context;

        public UnitOfWork(SearcherContext context)
        {
            _context = context;
            _repositories = new ConcurrentDictionary<Type, object>();
            _disposed = false;
        }

        public IGenericRepository<TEntity> GetRepository<TEntity>() where TEntity : class
        {
            return
                _repositories.GetOrAdd(typeof(TEntity),
                    x => new GenericRepository<TEntity>(_context)) as IGenericRepository<TEntity>;
        }

        public async Task SaveChangesAsync()
        {
            try
            {
                await _context.SaveChangesAsync();
            }
            catch (Exception e)
            {
                throw e.GetBaseException();
            }
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        public virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }

        public T GetContext<T>() where T : DbContext
        {
            return (T)Context;
        }
    }
}
