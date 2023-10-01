namespace AmazonClone.Api.Models
{
    public class ProductUpdateModel
    {
        public Guid Id { get; set; }
        public string NameEn { get; set; } = string.Empty;
        public string NameAr { get; set; } = string.Empty;
        public decimal UnitPrice { get; set; }
        public int StockQuantity { get; set; }
        public Guid CategoryId { get; set; }
    }
}
