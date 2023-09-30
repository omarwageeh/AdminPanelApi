using System.Linq.Expressions;

namespace AmazonClone.Repository.Interface
{
    public interface IRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAll();
        Task<IEnumerable<TEntity>> GetAll(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity?> Get(Expression<Func<TEntity, bool>> predicate);
        Task<TEntity> Add (TEntity entity);
        bool Update(TEntity entity);
        void Delete(TEntity entity);
    }
}
