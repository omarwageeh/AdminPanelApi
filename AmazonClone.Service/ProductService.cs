using AmazonClone.Dto;
using AmazonClone.Model;
using AmazonClone.Repository.Interface;
using System.Linq.Expressions;

namespace AmazonClone.Service
{
    public class ProductService : IProductService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Product>?> GetProducts(Expression<Func<Product, bool>> predicate)
        {
            return await _unitOfWork.ProductRepository.GetAllWithInclude(predicate, "Category");
        }
        public async Task AddProduct(Product product)
        {
            await _unitOfWork.ProductRepository.Add(product);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateProduct(Product product)
        {
            _unitOfWork.ProductRepository.Update(product);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteProduct(Guid id)
        {
            var product = await _unitOfWork.ProductRepository.Get(p => p.Id == id);
            if (product == null) return;
            _unitOfWork.ProductRepository.Delete(product);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
