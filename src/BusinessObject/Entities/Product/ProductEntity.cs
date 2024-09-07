using BusinessObject.Entities.Base;
using BusinessObject.Entities.Category;
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

namespace BusinessObject.Entities.Product
{
    [Table("ProductEntity")]
    [Index(nameof(ProductId), IsUnique = true, Name = "Index_ProductId")]
    public class ProductEntity : BaseEntity
    {
        public ProductEntity()
        {
            CreatedTime = LastUpdatedTime = CoreHelper.SystemTimeNow;
        }
        public int? ProductId { get; set; }
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
        public double? Hieght { get; set; }
        public virtual CategoryEntity CategoryEntity { get; set; }

        // Base Property
        public int? CreatedBy { get; set; }
        public int? LastUpdatedBy { get; set; }
        public int? DeletedBy { get; set; }
        public DateTimeOffset CreatedTime { get; set; }
        public DateTimeOffset LastUpdatedTime { get; set; }
        public DateTimeOffset? DeletedTime { get; set; }
    }
}
