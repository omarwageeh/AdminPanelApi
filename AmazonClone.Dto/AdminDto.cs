namespace AmazonClone.Dto
{
    public class AdminDto
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string JobTitle { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
    }
}
