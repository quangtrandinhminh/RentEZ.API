using Service.Models.Category;
using Service.Models.Shop;

namespace Service.Models.Product
{
    public class ProductResponse
    {
        public int ProductId { get; set; }
        public string? ProductName { get; set; }
        public int? CategoryId { get; set; }
        public string? CategoryName { get; set; }
        public int? ShopId { get; set; }
        public string? ShopName { get; set; }
        public double? Size { get; set; }
        public double? Price { get; set; }
        public double? RentPrice { get; set; }
        public double? DepositRate { get; set; }
        public double? Deposit { get; set; }
        public int? RentedCount { get; set; }
        public int? RatingCount { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double? Mass { get; set; }
        public double? Long { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
    }
}
