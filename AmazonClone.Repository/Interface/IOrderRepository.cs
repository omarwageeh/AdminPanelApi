using AmazonClone.Model;
using System.Linq.Expressions;

namespace AmazonClone.Repository.Interface
{
    public interface IOrderRepository : IRepository<Order>
    {
        Task<IEnumerable<Order>> GetAllWithInclude(Expression<Func<Order, bool>> predicate, string include);
    }
}
