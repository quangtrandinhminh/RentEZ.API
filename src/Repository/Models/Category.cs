using Repository.Models.Base;
using System.ComponentModel.DataAnnotations.Schema;

namespace Repository.Models
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Product> ProductEntities { get; set; } = new List<Product>();
    }
}
