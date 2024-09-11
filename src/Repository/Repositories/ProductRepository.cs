using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Infrastructure;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class ProductRepository : BaseRepository<Product> ,IProductRepository
    {
        private readonly AppDbContext _context = new();

        public ProductRepository(AppDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<Product>> GetAll()
        {
            return await _context.Products
                .AsNoTracking()
                .Select(x => new Product
                {
                    ProductName = x.ProductName,
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
                    Height = x.Height,
                    CreatedBy = x.CreatedBy,
                    CreatedTime = x.CreatedTime,
                    LastUpdatedBy = x.LastUpdatedBy,
                    LastUpdatedTime = x.LastUpdatedTime,
                    DeletedBy = x.DeletedBy,
                    DeletedTime = x.DeletedTime,
                    Category = new Category
                    {
                        CategoryName = x.Category.CategoryName,
                        Description = x.Category.Description
                    }
                })
                .ToListAsync();
        }

        public async Task<Product> GetById(int id)
        {
            return await _context.Products.AsNoTracking().Include(x => x.Category).FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
    }
}
