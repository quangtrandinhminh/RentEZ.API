using BusinessObject.Entities.Base;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;
using Utility.Helpers;

namespace BusinessObject.Entities
{
    public class Product : BaseEntity
    {
        public string? ProductName { get; set; }
        public int? CategoryId { get; set; }
        public double? Size { get; set; }
        public double? Price { get; set; }
        public double? RentPrice { get; set; }
        public int? RentedCount { get; set; }
        public int? RatingCount { get; set; }
        public string? Description { get; set; }
        public string? Image { get; set; }
        public double? Mass { get; set; }
        public double? Long { get; set; }
        public double? Width { get; set; }
        public double? Height { get; set; }
        public virtual Category Category { get; set; }
    }
}
