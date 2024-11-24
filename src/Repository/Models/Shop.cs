using System.ComponentModel.DataAnnotations;
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
        public int OwnerId { get; set; }
        [ForeignKey(nameof(OwnerId))]
        public virtual UserEntity Owner { get; set; }
        public virtual ICollection<Product> ProductEntities { get; set; } = new List<Product>();

        public decimal Rating => ProductEntities.Count == 0 ? 0 : ProductEntities.Average(x => x.Rating);

        [MaxLength(6)]
        public int? BankId { get; set; }

        [MaxLength(19)]
        public String? AccountNo { get; set; }

        [MaxLength(255)]
        public String? AccountName { get; set; }
    }
}
