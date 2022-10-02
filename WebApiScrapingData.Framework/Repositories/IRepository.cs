using System.Linq.Expressions;
using WebApiScrapingData.Framework;

namespace WebApiScrapingData.Core.Repositories
{
    /// <summary>
    /// Used to define the class of a Repository
    /// </summary>
    public interface IRepository<TEntity> where TEntity : class
    {
        IUnitOfWork UnitOfWork { get; }
        
        TEntity Get(int id);
        IQueryable<TEntity> Query();
        IEnumerable<TEntity> GetAll();
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> predicate);

        void Add(TEntity entity);
        void AddRange(IEnumerable<TEntity> entities);
        void SaveJsonInDb(string json);

        void Edit(TEntity entity);
        void EditRange(IEnumerable<TEntity> entities);

        void Remove(TEntity entity);
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}