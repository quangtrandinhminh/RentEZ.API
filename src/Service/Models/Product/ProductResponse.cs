namespace Service.Models.Product
{
    public class ProductResponse
    {
        public string? ProductName { get; set; }
        public string? CategoryName { get; set; }
        public double? Size { get; set; }
        public double? Price { get; set; }
        public double? RentPrice { get; set; }
        public int? RentedCount { get; set; }
        public int? RatingCount { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double? Mass { get; set; }
        public double? Long { get; set; }
        public double? Width { get; set; }
        public double? Hieght { get; set; }
    }
}
