
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace AmazonClone.Model
{
    public class User : IdentityUser<Guid>
    {
        [Required]
        public string FullName { get; set; }
        public bool isActive { get; set; }
        //public User(string fullName)
        //{
        //    FullName = fullName;
        //}
    }
}
