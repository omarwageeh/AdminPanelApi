namespace AmazonClone.Model
{
    public class Admin : User
    {
        public string? JobTitle { get; set; }
        public DateTime HireDate { get; set; } = DateTime.Now;
        public Admin()
        {
            
        }
    }
}
