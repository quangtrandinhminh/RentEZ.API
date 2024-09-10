using BusinessObject.Entities.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Helpers;
using BusinessObject.Entities.Identity;

namespace BusinessObject.Entities.Shop
{
    public class ShopEntity  : BaseEntity
    {
        public ShopEntity()
        {
            CreatedTime = LastUpdatedTime = CoreHelper.SystemTimeNow;
        }
        public string? ShopEmail { get; set; }
        public string? ShopName { get; set; }
        public string? Shop_Phone { get; set; }
        public string? Shop_Address { get; set; }
        public string? Shop_Avatar { get; set; }
        public bool Status { get; set; }
        public int? OwnerId { get; set; } 
        [ForeignKey(nameof(OwnerId))]
        public UserEntity? Owner { get; set; }

        //// Base Property
        //public int? CreatedBy { get; set; }
        //public int? LastUpdatedBy { get; set; }
        //public int? DeletedBy { get; set; }
        //public DateTimeOffset CreatedTime { get; set; }
        //public DateTimeOffset LastUpdatedTime { get; set; }
        //public DateTimeOffset? DeletedTime { get; set; }
    }
}
