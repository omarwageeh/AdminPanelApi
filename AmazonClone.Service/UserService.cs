using AmazonClone.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AmazonClone.Service
{
    public class UserService : IUserService
    {
        private readonly UserManager<Admin> _userManager;
        public UserService(UserManager<Admin> userManager)
        {
            _userManager = userManager;
        }
        public async Task<bool> Register(Admin admin, string password)
        {
            var result = await _userManager.CreateAsync(admin, password);
            if (result.Succeeded)
            {
                return true;
            }
            return false;
        }
        public async Task<User?> LoginUser(string email, string password)
        {
            var user = await _userManager.FindByNameAsync(email);
            var isLogged = await _userManager.CheckPasswordAsync(user, password);
            if (isLogged)
            {
                return user;
            }
            return null;
        }
    }
}