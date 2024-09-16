using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessObject.DTO.Shopkeeper
{
    public class ShopkeeperRequestDto
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string Address { get; set; } = string.Empty;
        public string Avatar { get; set; } = string.Empty;
    }
}
