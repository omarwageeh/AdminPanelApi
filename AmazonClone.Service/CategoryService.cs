using AmazonClone.Model;
using AmazonClone.Repository.Interface;
using System.Linq.Expressions;

namespace AmazonClone.Service
{
    public class CategoryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CategoryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Category>?> GetCategories()
        {
            return await _unitOfWork.CategoryRepository.GetAll();
        }
        public async Task<IEnumerable<Category>?> GetCategories(Expression<Func<Category, bool>> predicate)
        {
            return await _unitOfWork.CategoryRepository.GetAll(predicate);
        }
        public async Task AddCategory(Category category)
        {
            await _unitOfWork.CategoryRepository.Add(category);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task UpdateCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Update(category);
            await _unitOfWork.SaveChangesAsync();
        }
        public async Task DeleteCategory(Category category)
        {
            _unitOfWork.CategoryRepository.Delete(category);
            await _unitOfWork.SaveChangesAsync();
        }
    }
}
