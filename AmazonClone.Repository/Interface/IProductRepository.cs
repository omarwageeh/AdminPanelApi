using AmazonClone.Model;
using System.Linq.Expressions;

namespace AmazonClone.Repository.Interface
{
    public interface IProductRepository : IRepository<Product>
    {
        Task<IEnumerable<Product>> GetAllWithInclude(Expression<Func<Product, bool>> predicate, string include);
    }
}
