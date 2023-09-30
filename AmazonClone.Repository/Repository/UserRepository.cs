using AmazonClone.Data.Context;
using AmazonClone.Model;
using AmazonClone.Repository.Interface;

namespace AmazonClone.Repository.Repository
{
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(DataContext context) : base(context)
        {
        }
    }
}
