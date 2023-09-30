namespace AmazonClone.Dto
{
    public class ProductDto
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public string CategoryId { get; set; }
        public CategoryDto Category { get; set; }

    }
}
