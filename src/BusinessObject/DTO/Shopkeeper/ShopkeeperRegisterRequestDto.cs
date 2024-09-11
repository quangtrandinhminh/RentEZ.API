using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObject.DTO.User;

namespace BusinessObject.DTO.Shopkeeper
{
    public class ShopkeeperRegisterRequestDto : RegisterDto
    {
        // user
        public DateTime BirthDate { get; set; }

        // shop
        public string ShopEmail { get; set; }
        public string ShopName { get; set; }
        public string Shop_Phone { get; set; }
        public string Shop_Address { get; set; }
        public string? Shop_Avatar { get; set; }
    }
}
