using System.ComponentModel.DataAnnotations.Schema;
using Repository.Models.Base;
using Repository.Models.Identity;

namespace Repository.Models
{
    public class Shop : BaseEntity
    {
        public string? ShopEmail { get; set; }
        public string? ShopName { get; set; }
        public string? ShopPhone { get; set; }
        public string? ShopAddress { get; set; }
        public string? ShopAvatar { get; set; }
        public DateTimeOffset? Verified { get; set; }
        public bool IsVerified => Verified.HasValue;

        // Is Active if Owner is Active and Shop is Verified
        public bool IsActive => Owner?.IsActive == true && IsVerified;
        public int? OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public UserEntity? Owner { get; set; }
        public virtual ICollection<Product> ProductEntities { get; set; } = new List<Product>();
        public virtual ICollection<Voucher> Vouchers { get; set; } = new List<Voucher>();
    }
}
