using Microsoft.AspNetCore.Http;
using Repository.Models;
using Utility.Constants;
using Utility.Enum;
using Utility.Exceptions;
using Utility.Helpers;

namespace Service.BusinessModels;

public class OrderShopProcessor(OrderShop orderShop)
{
    public void ChangeNextStatus()
    {
        switch (orderShop.Status)
        {
            case OrderShopStatusEnum.WaitForPayment:
                Pending();
                break;
            case OrderShopStatusEnum.Pending:
                Approve();
                break;
            case OrderShopStatusEnum.Approved:
                Shipping();
                break;
            case OrderShopStatusEnum.Shipping:
                Received();
                break;
            case OrderShopStatusEnum.Received:
                Returning();
                break;
            case OrderShopStatusEnum.Returning:
                Returned();
                break;
            case OrderShopStatusEnum.Returned:
                WaitingForRefund();
                break;
            case OrderShopStatusEnum.ReturnedWithDamage:
                WaitingForRefund();
                break;
            case OrderShopStatusEnum.WaitingForRefund:
                Completed();
                break;
            default:
                throw new AppException(ResponseCodeConstants.BAD_REQUEST, "Invalid status", StatusCodes.Status400BadRequest);
        }

        AddOrderTracking();
    }

    public void Reject()
    {
        if (orderShop.Status == OrderShopStatusEnum.Pending)
        {
            orderShop.Status = OrderShopStatusEnum.Rejected;
            AddOrderTracking();
        }
        else
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, 
                ResponseMessageConstrantsOrder.STATUS_CHANGE_NOTALLOWED, StatusCodes.Status400BadRequest);
        }
    }

    public void Cancel()
    {
        if (orderShop.Status == OrderShopStatusEnum.WaitForPayment)
        {
            orderShop.Status = OrderShopStatusEnum.Cancelled;
            AddOrderTracking();
        }
        else
        {
            throw new AppException(ResponseCodeConstants.BAD_REQUEST, 
                               ResponseMessageConstrantsOrder.STATUS_CHANGE_NOTALLOWED, StatusCodes.Status400BadRequest);
        }
    }
    private void Pending()
    {
        orderShop.Status = OrderShopStatusEnum.Pending;
    }
    private void Approve()
    {
        orderShop.Status = OrderShopStatusEnum.Approved;
    }

    private void Shipping()
    {
        orderShop.Status = OrderShopStatusEnum.Shipping;
    }

    private void Received()
    {
        orderShop.Status = OrderShopStatusEnum.Received;
    }

    private void Returning()
    {
        orderShop.Status = OrderShopStatusEnum.Returning;
    }

    private void Returned()
    {
        orderShop.Status = OrderShopStatusEnum.Returned;
    }

    private void ReturnedWithDamage()
    {
        orderShop.Status = OrderShopStatusEnum.ReturnedWithDamage;
    }

    private void WaitingForRefund()
    {
        orderShop.Status = OrderShopStatusEnum.WaitingForRefund;
    }

    private void Completed()
    {
        orderShop.Status = OrderShopStatusEnum.Completed;
    }

    private void AddOrderTracking()
    {
        orderShop.OrderTrackings.Add(new OrderTracking
        {
            Status = orderShop.Status.ToString(),
            OrderShopId = orderShop.Id,
        });
    }
}