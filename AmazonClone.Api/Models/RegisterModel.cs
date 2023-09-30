namespace AmazonClone.Api.Models
{
    public class RegisterModel
    {
        public string Email { get; set; } = string.Empty;
        public string FullName { get; set; } = string.Empty;
        public bool IsActive { get; set; }
        public string? JobTitle { get; set; }
        public string Password { get; set; } = string.Empty;
    }
}
