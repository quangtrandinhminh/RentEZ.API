using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service.Models.Category
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
