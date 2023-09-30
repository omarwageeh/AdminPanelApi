using AmazonClone.Data.Context;
using AmazonClone.Model;
using AmazonClone.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AmazonClone.Repository.Repository
{
    public class OrderRepository : GenericRepository<Order>, IOrderRepository
    {
        private readonly DataContext _context;
        public OrderRepository(DataContext context) : base(context)
        {
            _context = context;
        }


        public async Task<IEnumerable<Order>> GetAllWithInclude(Expression<Func<Order, bool>> predicate, string include)
        {
            return  await _context.Set<Order>().Where(predicate).Include(include).ToListAsync();
        }
    }
}
