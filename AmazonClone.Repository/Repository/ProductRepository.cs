using AmazonClone.Data.Context;
using AmazonClone.Model;
using AmazonClone.Repository.Interface;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace AmazonClone.Repository.Repository
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        private readonly DataContext _context;
        public ProductRepository(DataContext context) : base(context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Product>> GetAllWithInclude(Expression<Func<Product, bool>> predicate, string include = "Category")
        {
            return await _context.Set<Product>().Where(predicate).Include(include).ToListAsync();
        }
    }
}
