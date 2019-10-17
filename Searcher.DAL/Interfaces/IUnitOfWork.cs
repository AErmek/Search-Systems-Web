using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace Searcher.DAL.Interfaces
{
    public interface IUnitOfWork : IDisposable
    {
        IGenericRepository<T> GetRepository<T>() where T : class;

        Task SaveChangesAsync();

        void Dispose(bool disposing);

        T GetContext<T>() where T : DbContext;

    }
}
