using BusinessObject.Entities;
using Microsoft.EntityFrameworkCore;
using Repository.Base;
using Repository.Infrastructure;
using Repository.Interfaces;

namespace Repository.Repositories
{
    public class ShopRepository(AppDbContext context)
        : BaseRepository<Shop>(context), IShopRepository;
}
    