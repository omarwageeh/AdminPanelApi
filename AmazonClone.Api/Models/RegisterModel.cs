namespace AmazonClone.Api.Models
{
    public class RegisterModel
    {
        public string Email { get; set; }
        public string FullName { get; set; }
        public bool isActive { get; set; }
        public string? JobTitle { get; set; }
        public string password { get; set; }
    }
}
