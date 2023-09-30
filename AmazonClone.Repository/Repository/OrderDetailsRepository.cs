using AmazonClone.Data.Context;
using AmazonClone.Model;
using AmazonClone.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace AmazonClone.Repository.Repository
{
    public class OrderDetailsRepository : GenericRepository<OrderDetails>, IOrderDetailsRepository
    {
        private readonly DataContext _context;
        public OrderDetailsRepository(DataContext context) : base(context)
        {
            _context = context;
        }
        public async Task<IEnumerable<OrderDetails?>> GetAllWithInclude(Expression<Func<OrderDetails, bool>> predicate, string include)
        {
            return await _context.Set<OrderDetails>().Where(predicate).Include(include).ToListAsync();
        }
    }
}
