using System.ComponentModel.DataAnnotations;
using Utility.Constants;

namespace Service.ApiModels.Shop
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
        [Range(1, 999999, ErrorMessage = "Bank Id must be a positive number with up to 6 digits.")]
        public int BankId { get; set; }
        [Required]
        [MaxLength(19)]
        public String AccountNo { get; set; }
        [Required]
        [MaxLength(255)]
        public String AccountName { get; set; }
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
