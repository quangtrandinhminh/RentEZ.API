using System.ComponentModel.DataAnnotations;
using Utility.Constants;

namespace Service.Models.Shop
{
    public class ShopCreateRequest
    {

        [Required(ErrorMessage = ResponseMessageIdentity.EMAIL_REQUIRED)]
        [EmailAddress(ErrorMessage = "Invalid email format")]
        public string ShopEmail { get; set; }
        [Required(ErrorMessage = ResponseMessageIdentity.NAME_REQUIRED)]
        [MaxLength(100)]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Name cannot contain number")]
        public string ShopName { get; set; }
        [Required]
        public string ShopAddress { get; set; }
        [Required]
        public string ShopPhone { get; set; }
        [Required]
        public string ShopAvatar { get; set; }
        [Required]
        public int OwnerId { get; set; }
    }
}
