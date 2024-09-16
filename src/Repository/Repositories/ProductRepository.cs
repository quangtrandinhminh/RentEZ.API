using Repository.Base;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Repositories
{
    public class ProductRepository(AppDbContext context)
        : BaseRepository<Product>(context), IProductRepository;
}
