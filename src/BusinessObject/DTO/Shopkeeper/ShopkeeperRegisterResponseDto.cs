using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO.Shopkeeper
{
    public class ShopkeeperRegisterResponseDto
    {
        // user
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public string? PhoneNumber { get; set; }
        public string? FullName { get; set; }
        public string? Address { get; set; }
        public string? Avatar { get; set; }
        public DateTimeOffset? BirthDate { get; set; }

        // shop
        public string? ShopEmail { get; set; }
        public string? ShopName { get; set; }
        public string? Shop_Phone { get; set; }
        public string? Shop_Address { get; set; }
        public string? Shop_Avatar { get; set; }
        public bool Status { get; set; }
    }
}
