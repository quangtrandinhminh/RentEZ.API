using Repository.Base;
using Repository.Extensions;
using Repository.Models;
using System.Linq.Expressions;

namespace Repository.Interfaces;

public interface IOrderShopRepository : IBaseRepository<OrderShop>
{
    Task<OrderShop> GetOrderShopByIdAsync(
        Expression<Func<OrderShop, bool>>? predicate = null,
        bool isIncludeDeleted = false,
        bool isIncludeProducts = false,
        params Expression<Func<OrderShop, object>>[] includeProperties);

    Task<PaginatedList<OrderShop>> GetAllOrderShopsAsync(int pageIndex, int pageSize,
        Expression<Func<OrderShop, bool>>? predicate = null,
        bool isIncludeDeleted = false,
        bool isIncludeProducts = false,
        Expression<Func<OrderShop, object>>? orderBy = null,
        params Expression<Func<OrderShop, object>>[] includeProperties);
}