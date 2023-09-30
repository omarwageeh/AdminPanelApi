namespace AmazonClone.Model
{
    public class OrderDetails
    {
        public Guid OrderId { get; set; }
        public virtual Order Order { get; set; } = null!;
        public Guid ProductId { get; set; }
        public virtual Product Product { get; set; } = null!;
        public int ProductCount { get; set; }
        public decimal UnitPrice { get; set; }
    }
}
