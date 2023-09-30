using AmazonClone.Data.Context;
using AmazonClone.Model;
using AmazonClone.Repository.Interface;

namespace AmazonClone.Repository.Repository
{
    public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
    {
        public CategoryRepository(DataContext context) : base(context)
        {
        }
    }
}
