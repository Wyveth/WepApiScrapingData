using System.Linq.Expressions;
using WebApiScrapingData.Domain.Interface;
using WebApiScrapingData.Framework;

namespace WebApiScrapingData.Core.Repositories
{
    /// <summary>
    /// Used to define the class of a Repository
    /// </summary>
    public interface IRepository<T> where T : class, ITIdentity
    {
        IUnitOfWork UnitOfWork { get; }

        Task<T?> Get(long id);
        IQueryable<T> Query();
        Task<IEnumerable<T>> GetAll();
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> predicate);

        Task<T?> SingleOrDefault(Expression<Func<T, bool>> predicate);

        Task<bool> Add(T entity);
        Task<bool> AddRange(IEnumerable<T> entities);

        bool Update(T entity);
        bool UpdateRange(IEnumerable<T> entities);

        bool Remove(T entity);
        bool RemoveRange(IEnumerable<T> entities);

        Task SaveJsonInDb(string json);
    }
}