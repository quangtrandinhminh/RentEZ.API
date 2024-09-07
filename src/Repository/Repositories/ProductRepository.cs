using BusinessObject.Entities.Category;
using BusinessObject.Entities.Product;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Infrastructure;
using Repository.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository.Repositories
{
    public class ProductRepository : BaseRepository<ProductEntity> ,IProductRepository
    {
        private readonly AppDbContext _context = new();

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<ProductEntity>> GetAll()
        {
            return await _context.Products
                .AsNoTracking()
                .Select(x => new ProductEntity
                {
                    ProductId = x.ProductId,
                    Size = x.Size,
                    Price = x.Price,
                    RentPrice = x.RentPrice,
                    RentedCount = x.RentedCount,
                    RatingCount = x.RatingCount,
                    Description = x.Description,
                    Image = x.Image,
                    Mass = x.Mass,
                    Long = x.Long,
                    Width = x.Width,
                    Hieght = x.Hieght,
                    CreatedBy = x.CreatedBy,
                    CreatedTime = x.CreatedTime,
                    LastUpdatedBy = x.LastUpdatedBy,
                    LastUpdatedTime = x.LastUpdatedTime,
                    DeletedBy = x.DeletedBy,
                    DeletedTime = x.DeletedTime,
                    CategoryEntity = new CategoryEntity
                    {
                        CategoryId = x.CategoryId,
                        CategoryName = x.CategoryEntity.CategoryName,
                        Description = x.CategoryEntity.Description
                    }
                })
                .ToListAsync();
        }

        public async Task<ProductEntity> GetById(int id)
        {
            return await _context.Products.AsNoTracking().Include(x => x.CategoryEntity).FirstOrDefaultAsync(x => x.ProductId.Equals(id));
        }
    }
}
