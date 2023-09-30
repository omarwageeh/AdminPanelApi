using AmazonClone.Model;
using System.Linq.Expressions;

namespace AmazonClone.Repository.Interface
{
    public interface IOrderDetailsRepository : IRepository<OrderDetails>
    {
        Task<IEnumerable<OrderDetails?>> GetAllWithInclude(Expression<Func<OrderDetails, bool>> predicate, string include);
    }
}
