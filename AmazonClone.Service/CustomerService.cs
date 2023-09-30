using AmazonClone.Model;
using AmazonClone.Repository.Interface;
using System.Linq.Expressions;

namespace AmazonClone.Service
{
    
    public class CustomerService
    {
        private readonly IUnitOfWork _uow;
        public CustomerService(IUnitOfWork uow)
        {
            _uow = uow;
        }


        public async Task<IEnumerable<Order>?> GetOrders(Expression<Func<Order, bool>> predicate)
        {
            return await _uow.OrderRepository.GetAllWithInclude(predicate, "Customer");
        }

    }
}
