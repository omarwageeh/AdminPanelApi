using AmazonClone.Model;

namespace AmazonClone.Service
{
    public interface IUserService
    {
        Task<User?> LoginUser(string email, string password);
        Task<bool> Register(Admin admin, string password);
    }
}