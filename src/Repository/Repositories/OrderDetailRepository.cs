using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Extensions;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Models;

namespace Repository.Repositories;

public class OrderDetailRepository(IServiceProvider serviceProvider) : IOrderDetailRepository
{
    private readonly AppDbContext _dbContext = serviceProvider.GetRequiredService<AppDbContext>();

    public async Task<OrderDetail> GetSingleAsync(Expression<Func<OrderDetail, bool>> predicate,
        bool isIncludeCustomer = false,
        params Expression<Func<OrderDetail, object>>[] includeProperties)
    {
        var query = _dbContext.Set<OrderDetail>().AsNoTracking();

        foreach (var includeProperty in includeProperties)
        {
            if (isIncludeCustomer)
            {
                query = query.Include(x => x.OrderShop)
                    .ThenInclude(x => x.Order)
                    .ThenInclude(x => x.Customer);
            }

            query = query.Include(includeProperty);
        }

        return await query.FirstOrDefaultAsync(predicate);
    }

    public void UpdateOrderDetail(OrderDetail entity)
    {
        _dbContext.Entry(entity).State = EntityState.Modified;
        _dbContext.SaveChanges();
    }

    public Task<FeedbackResponse?> GetFeedbackResponseAsync(int orderDetailId)
    {
        return _dbContext.Set<OrderDetail>().AsNoTracking()
            .Where(x => x.Id == orderDetailId)
            .Select(x => new FeedbackResponse
            {
                OrderDetailId = x.Id,
                Rating = x.Rating ?? 0,
                ReviewContent = x.ReviewContent,
            })
            .FirstOrDefaultAsync();
    }

    public async Task<PaginatedList<FeedbackResponse>> GetFeedbackList(int pageNumber, int pageSize,
        bool isIncludeShop = false,
        bool isIncludeCustomer = false,
        Expression<Func<OrderDetail, bool>> predicate = null,
        params Expression<Func<OrderDetail, object>>[] includeProperties)
        
    {
        var query = _dbContext.Set<OrderDetail>().AsNoTracking();

        foreach (var includeProperty in includeProperties)
        {
            if (isIncludeShop)
            {
                query = query.Include(x => x.Product).ThenInclude(x => x.Shop);
            }

            if (isIncludeCustomer)
            {
                query = query.Include(x => x.OrderShop)
                    .ThenInclude(x => x.Order)
                    .ThenInclude(x => x.Customer);
            }

            query = query.Include(includeProperty);
        }

        query = predicate != null ? query.Where(predicate) : query;
        query.OrderByDescending(x => x.FeedbackCreatedTime);

        return await PaginatedList<FeedbackResponse>.CreateAsync(query.Select(x => new FeedbackResponse
        {
            CustomerId = x.OrderShop.Order.CustomerId,
            CustomerName = x.OrderShop.Order.Customer.UserName,
            Avatar = x.OrderShop.Order.Customer.Avatar,
            ProductId = x.ProductId,
            ProductName = x.Product.ProductName,
            OrderDetailId = x.Id,
            Rating = x.Rating ?? 0,
            ReviewContent = x.ReviewContent,
            FeedbackCreatedTime = x.FeedbackCreatedTime,
            FeedbackLastUpdatedTime = x.FeedbackLastUpdatedTime
        }), pageNumber, pageSize);
    }
}