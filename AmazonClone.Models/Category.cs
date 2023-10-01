using System.ComponentModel.DataAnnotations;

namespace AmazonClone.Model
{
    public class Category : BaseEntity
    {
        [Required]
        public string Name { get; set; }
        public virtual ICollection<Product> Products { get; set; }
        public Category(string name) 
        {
            Name = name;
            Products = new HashSet<Product>();
        }
        public Category() 
        {
            Products = new HashSet<Product>();
        }
    }
}
