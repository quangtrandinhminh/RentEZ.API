using System.ComponentModel.DataAnnotations;

namespace Service.ApiModels.Category
{
    public class CategoryCreateRequest
    {
        [Required]
        [MaxLength(100)]
        [RegularExpression("^[^0-9]+$", ErrorMessage = "Name cannot contain number")]
        public string? CategoryName { get; set; }
        [Required]
        public string? Description { get; set; }
    }
}
