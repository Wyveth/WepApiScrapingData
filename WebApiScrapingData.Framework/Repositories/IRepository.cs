using System.Linq.Expressions;
using WebApiScrapingData.Domain.Interface;
using WebApiScrapingData.Framework;

namespace WebApiScrapingData.Core.Repositories
{
    /// <summary>
    /// Used to define the class of a Repository
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class, ITIdentity
    {
        IUnitOfWork UnitOfWork { get; }

        Task<TEntity?> Get(int id);
        IQueryable<TEntity> Query();
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> Find(Expression<Func<TEntity, bool>> predicate);

        Task<TEntity?> SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        Task Add(TEntity entity);
        Task AddRange(IEnumerable<TEntity> entities);
        Task SaveJsonInDb(string json);

        void Edit(TEntity entity);
        void EditRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}