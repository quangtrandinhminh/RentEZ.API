using BusinessObject.Entities.Base;
using BusinessObject.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace BusinessObject.Entities
{
    [Table("Categories")]
    public class Category : BaseEntity
    {
        public string? CategoryName { get; set; }
        public string? Description { get; set; }
        public virtual ICollection<Product> ProductEntities { get; set; } = new List<Product>();
    }
}
