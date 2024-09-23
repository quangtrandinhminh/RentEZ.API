using System.ComponentModel.DataAnnotations;

namespace Service.Models.Product
{
    public class ProductCreateRequestDto
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Name cannot contain number")]
        public string ProductName { get; set; }
        [Required]
        public int CategoryId { get; set; }

        // [Required]
        // public int ShopId { get; set; }

        [Required]
        [RegularExpression(@"^[+]?[0]*[1-9]\d*(\.\d+)?$", ErrorMessage = "Only natural numbers or real numbers greater than 0 are allowed")]
        public double Size { get; set; }
        [Required]
        [RegularExpression(@"^[+]?[0]*[1-9]\d*(\.\d+)?$", ErrorMessage = "Only natural numbers or real numbers greater than 0 are allowed")]
        public double Price { get; set; }
        [Required]
        [RegularExpression(@"^[+]?[0]*[1-9]\d*(\.\d+)?$", ErrorMessage = "Only natural numbers or real numbers greater than 0 are allowed")]
        public double RentPrice { get; set; }
        [Required]
        public string Description { get; set; }
        [Required]
        //[DataType(DataType.ImageUrl)]
        public string Image { get; set; }
        [Required]
        [RegularExpression(@"^[+]?[0]*[1-9]\d*(\.\d+)?$", ErrorMessage = "Only natural numbers or real numbers greater than 0 are allowed")]
        public double Mass { get; set; }
        [Required]
        [RegularExpression(@"^[+]?[0]*[1-9]\d*(\.\d+)?$", ErrorMessage = "Only natural numbers or real numbers greater than 0 are allowed")]
        public double Long { get; set; }
        [Required]
        [RegularExpression(@"^[+]?[0]*[1-9]\d*(\.\d+)?$", ErrorMessage = "Only natural numbers or real numbers greater than 0 are allowed")]
        public double Width { get; set; }
        [Required]
        [RegularExpression(@"^[+]?[0]*[1-9]\d*(\.\d+)?$", ErrorMessage = "Only natural numbers or real numbers greater than 0 are allowed")]
        public double Height { get; set; }
    }
}
