using Repository.Base;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Repositories
{
    public class CategoryRepository(AppDbContext context)
        : BaseRepository<Category>(context), ICategoryRepository;
}
