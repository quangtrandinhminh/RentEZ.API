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

namespace BusinessObject.Entities
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
    }
}
