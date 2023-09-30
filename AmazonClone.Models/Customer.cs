using System.ComponentModel.DataAnnotations;

namespace AmazonClone.Model
{
    public class Customer : User
    {
        [MaxLength(12)]
        public string? Phone { get; set; }
        [Required]
        public string Address { get; set; }
        public Customer(string address) : base()
        {
            Address = address;
        }
        public Customer() : base()
        {
            Address = "Address";
        }
    }
}
