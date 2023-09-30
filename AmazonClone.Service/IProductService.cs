using AmazonClone.Model;
using System.Linq.Expressions;

namespace AmazonClone.Service
{
    public interface IProductService
    {
        Task AddProduct(Product product);
        Task DeleteProduct(Guid id);
        Task<IEnumerable<Product>?> GetProducts(Expression<Func<Product, bool>> predicate);
        Task UpdateProduct(Product product);
    }
}