using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Models.Base;

namespace Repository.Models
{
    public class Product : BaseEntity
    {
        public string? ProductName { get; set; }
        public int CategoryId { get; set; }
        public int ShopId { get; set; }
        public double? Size { get; set; }
        public double? Price { get; set; }
        public string? RentPrice { get; set; }
        public int RentedCount { get; set; } = 0;
        public int RatingCount { get; set; } = 0;
        public int Quantity { get; set; } = 0;
        public int? AllowRentBeforeDays { get; set; }
        public int? Construction {  get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double? Mass { get; set; }
        public double? Long { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }

        [Range(0,1)]
        public double? DepositRate { get; set; }
        public double? Deposit => DepositRate * Price;

        [ForeignKey(nameof(CategoryId))]
        public virtual Category Category { get; set; }

        [ForeignKey(nameof(ShopId))]
        public virtual Shop Shop { get; set; }

        public virtual ICollection<OrderDetail> OrderDetails { get; set; } = new List<OrderDetail>();
    }
}
