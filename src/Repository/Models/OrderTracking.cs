using System.ComponentModel.DataAnnotations.Schema;
using Utility.Enum;
using Utility.Helpers;

namespace Repository.Models;

public class OrderTracking
{
    public OrderTracking()
    {
        UpdatedAt = CoreHelper.SystemTimeNow;
    }

    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public int OrderTrackingId { get; set; }  
    public int OrderShopId { get; set; }              
    public string Status { get; set; }
    public DateTimeOffset UpdatedAt { get; private set; }

    [ForeignKey("OrderShopId")]                       
    public virtual OrderShop OrderShop { get; set; }
}