using System.ComponentModel.DataAnnotations;
using Utility.Constants;

namespace Service.ApiModels.Product
{
    public class ProductCreateRequest
    {
        [Required]
        [MaxLength(100)]
        //[RegularExpression("^[^0-9]+$", ErrorMessage = "Name cannot contain number")]
        public string ProductName { get; set; }
        [Required]
        public int CategoryId { get; set; }
        [Required]
        [Range(0,Int32.MaxValue, ErrorMessage = "Vui lòng nhập giá trị dương")]
        public int Quantity { get; set; }
        [Required]
        [Range(2, 5, ErrorMessage = ResponseMessageConstrantsProduct.INVALID_ALLOWRENTBEFOREDAYS)]
        public int AllowRentBeforeDays { get; set; }
        public int Construction = 5;
        [Required]
        [Range(0.0001, Double.MaxValue, ErrorMessage = "Vui lòng nhập giá trị lớn hơn 0")]
        public double Size { get; set; }
        [Required]
        [Range(0.0001, Double.MaxValue, ErrorMessage = "Vui lòng nhập giá trị lớn hơn 0")]
        public double Price { get; set; }
        [Required]
        public List<double> RentPrices { get; set; }
        [Required]
        [RegularExpression(@"^(0\.[0-7]|0\.[1-6]\d*)$", ErrorMessage = "Vui lòng nhập giá trị lớn hơn 0 và nhỏ hơn hoặc bằng 0.7")]
        public double DepositRate { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        //[DataType(DataType.ImageUrl)]
        public string Image { get; set; }

        [Range(0.0001, Double.MaxValue, ErrorMessage = "Vui lòng nhập giá trị lớn hơn 0")]
        public double? Mass { get; set; } = null;
        [Range(0.0001, Double.MaxValue, ErrorMessage = "Vui lòng nhập giá trị lớn hơn 0")]
        public double? Long { get; set; } = null;
        [Range(0.0001, Double.MaxValue, ErrorMessage = "Vui lòng nhập giá trị lớn hơn 0")]
        public double? Width { get; set; } = null;
        [Range(0.0001, Double.MaxValue, ErrorMessage = "Vui lòng nhập giá trị lớn hơn 0")]
        public double? Height { get; set; } = null;
    }
}
