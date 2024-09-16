using Repository.Base;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Repositories
{
    public class ShopRepository(AppDbContext context)
        : BaseRepository<Shop>(context), IShopRepository;
}
    