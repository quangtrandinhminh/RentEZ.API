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
        public int RatingCount => OrderDetails.Count(x => x.Rating != null);
        public int Quantity { get; set; } = 0;
        public int? AllowRentBeforeDays { get; set; }
        public int? Construction {  get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double? Mass { get; set; }
        public double? Long { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public decimal Rating => RatingCalc();

        private decimal RatingCalc()
        {
            var totalRating = 0m;
            var total = OrderDetails.Count(x => x.Rating != null);
            if (total == 0)
            {
                return 0m;
            }

            foreach (var orderDetail in OrderDetails)
            {
                if (orderDetail.Rating.HasValue)
                {
                    totalRating += orderDetail.Rating.Value;
                }
            }

            return totalRating / total;
        }

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
