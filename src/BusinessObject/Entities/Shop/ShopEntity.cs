using BusinessObject.Entities.Base;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;

namespace BusinessObject.Entities.Shop
{
    public class ShopEntity  : BaseEntity
    {
        public ShopEntity()
        {
            CreatedTime = LastUpdatedTime = CoreHelper.SystemTimeNow;
        }

        public int? ShopId { get; set; }
        public string? ShopEmail { get; set; }
        public string? ShopName { get; set; }
        public string? Address { get; set; }
        public string? Owner_Avatar { get; set; }

        // Base Property
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }
    }
}
