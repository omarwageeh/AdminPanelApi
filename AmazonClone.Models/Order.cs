using AmazonClone.Model.Enum;

namespace AmazonClone.Model
{
    public class Order : BaseEntity
    {
        public decimal TotalPrice { get; set; }
        public Guid CustomerId { get; set; }
        public virtual Customer Customer { get; set; } = null!;
        public Status Status { get; set; }
    }
}
