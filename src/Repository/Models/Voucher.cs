using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Repository.Models.Base;
using Repository.Models.Identity;
using Utility.Enum;
using Utility.Helpers;

namespace Repository.Models;

public class Voucher : BaseEntity
{
    public int? ShopId { get; set; }
    public string? Code { get; set; }
    public string? Name { get; set; }

    [Range(0, Double.MaxValue)]
    public decimal Value { get; set; }

    [Range(0, Double.MaxValue)]
    public decimal? MinRentValue { get; set; }

    // for percent voucher
    [Range(0, Double.MaxValue)]
    public decimal? MaxDiscountValue { get; set; }
    public VoucherTypeEnum Type { get; set; }
    public VoucherValueTypeEnum ValueType { get; set; }
    public DateTimeOffset? StartDate { get; set; }
    public DateTimeOffset? EndDate { get; set; }

    [Range(0, Int32.MaxValue)]
    public int? Limit { get; set; } 
    public int Used { get; set; }
    public string? Description { get; set; } 
    public VoucherStatusEnum Status
    {
        get
        {
            if (IsPreparing)
            {
                return VoucherStatusEnum.Preparing;
            }

            if (IsExpired)
            {
                return VoucherStatusEnum.Expired;
            }

            if (IsLimited)
            {
                return VoucherStatusEnum.Limited;
            }

            return VoucherStatusEnum.Active;
        }
    }
    private bool IsPreparing => StartDate.HasValue && StartDate > CoreHelper.SystemTimeNow;
    private bool IsExpired => EndDate.HasValue && EndDate < CoreHelper.SystemTimeNow;
    private bool IsLimited => Limit.HasValue && Used >= Limit;

    [ForeignKey("ShopId")]
    public virtual Shop? Shop { get; set; }
    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
    public virtual ICollection<OrderShop> OrderShops { get; set; } = new List<OrderShop>();
    public virtual ICollection<UserEntity> Users { get; set; } = new List<UserEntity>();
}