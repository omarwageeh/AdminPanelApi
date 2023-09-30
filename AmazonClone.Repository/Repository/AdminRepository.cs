using AmazonClone.Data.Context;
using AmazonClone.Model;
using AmazonClone.Repository.Interface;

namespace AmazonClone.Repository.Repository
{
    public class AdminRepository : GenericRepository<Admin>, IAdminRepository
    {
        public AdminRepository(DataContext context) : base(context)
        {
        }
    }
}
