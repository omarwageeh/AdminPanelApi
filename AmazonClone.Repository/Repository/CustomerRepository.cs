using AmazonClone.Data.Context;
using AmazonClone.Model;
using AmazonClone.Repository.Interface;

namespace AmazonClone.Repository.Repository
{
    public class CustomerRepository : GenericRepository<Customer>, ICustomerRepository
    {
        public CustomerRepository(DataContext context) : base(context)
        {
        }
    }
}
