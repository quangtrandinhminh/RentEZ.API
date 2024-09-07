using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utility.Constants;

namespace BusinessObject.DTO.Shop
{
    public class ShopCreateRequestDto
    {
        [Required(ErrorMessage = ResponseMessageIdentity.EMAIL_REQUIRED)]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string? ShopEmail { get; set; }
        [Required(ErrorMessage = ResponseMessageIdentity.NAME_REQUIRED)]
        [MaxLength(100)]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Name cannot contain number")]
        public string? ShopName { get; set; }
        [Required]
        public string? Address { get; set; }
        [Required]
        public string? Owner_Avatar { get; set; }
    }
}
