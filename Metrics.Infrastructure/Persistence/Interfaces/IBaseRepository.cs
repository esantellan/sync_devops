using Microsoft.EntityFrameworkCore.Storage;
using System.Linq.Expressions;

namespace Metrics.Infrastructure.Persistence.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();
        T Insert(T entity);
        T Update(T entity);
        void Delete(int id);
        void Delete(T entity);

        #region Async
        Task<T> GetAsync(int id);
        Task<IEnumerable<T>> GetAllAsync();
        Task<List<T>> FindAsync(Expression<Func<T, bool>> predicate);
        Task<T> InsertAsync(T entity);
        Task InsertRangeAsync(IEnumerable<T> entities);
        Task UpdateAsync(T entity);
        Task DeleteAsync(int id);
        Task<IDbContextTransaction> BeginTransactionAsync();
        #endregion
    }
}
