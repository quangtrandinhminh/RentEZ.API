using Repository.Extensions;
using Repository.Models;
using System.Linq.Expressions;

namespace Repository.Interfaces;

public interface IOrderDetailRepository
{
    public Task<OrderDetail> GetSingleAsync(Expression<Func<OrderDetail, bool>> predicate,
        bool isIncludeCustomer = false,
        params Expression<Func<OrderDetail, object>>[] includeProperties);
    public void UpdateOrderDetail(OrderDetail entity);
    Task<FeedbackResponse?> GetFeedbackResponseAsync(int orderDetailId);
    Task<PaginatedList<FeedbackResponse>> GetFeedbackList(int pageNumber, int pageSize,
        bool isIncludeShop = false,
        bool isIncludeCustomer = false,
        Expression<Func<OrderDetail, bool>> predicate = null,
        params Expression<Func<OrderDetail, object>>[] includeProperties);
}