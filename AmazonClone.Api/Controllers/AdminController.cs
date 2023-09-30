using AmazonClone.Api.Models;
using AmazonClone.Model;
using AmazonClone.Service;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace AmazonClone.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AdminController : ControllerBase
    {
        private readonly IConfiguration _config;
        private readonly IUserService _userService;

        public AdminController(IConfiguration config, IUserService userService)
        {
            _config = config;
            _userService = userService;
        }

        [HttpPost("Login")]
        public async Task<IActionResult> Login([FromBody] UserModel login)
        {

            var user = await _userService.LoginUser(login.Email, login.Password);

            if (user != null)
            {
                var tokenString = GenerateJSONWebToken(login);
                return Ok(new { token = tokenString });
            }

            return Unauthorized();
        }
        [HttpPost("Register")]
        public async Task<IActionResult> Register(RegisterModel register)
        {
            var admin = new Admin()
            {
                UserName = register.Email,
                FullName = register.FullName,
                Email = register.Email,
                JobTitle = register.JobTitle,
            };
            var created = await _userService.Register(admin, register.password);
            if (created)
            {
                return Ok();
            }
            return BadRequest();
        }
        private string GenerateJSONWebToken(UserModel userInfo)
        {
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Jwt:Key"]));

            var claims = new[] {
                    new Claim(JwtRegisteredClaimNames.Email, userInfo.Email),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
                  };

            var token = new JwtSecurityToken(
                issuer: _config["Jwt:Issuer"],
                audience: _config["Jwt:Audience"],
                expires: DateTime.Now.AddHours(1),
                claims: claims,
                signingCredentials: new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256)
                );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
