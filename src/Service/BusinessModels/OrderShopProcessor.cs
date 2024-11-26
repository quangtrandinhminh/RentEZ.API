using Microsoft.AspNetCore.Http;
using Repository.Models;
using Utility.Constants;
using Utility.Enum;
using Utility.Exceptions;

namespace Service.BusinessModels;

public class OrderShopProcessor(OrderShop orderShop)
{
    public string ChangeNextStatus()
    {
        switch (orderShop.Status)
        {
            // system payment success
            case OrderShopStatusEnum.WaitForPayment:
                Pending();
                break;

            // shop accepted
            case OrderShopStatusEnum.Pending:
                Approve();
                break;

            // shop shipped
            case OrderShopStatusEnum.Approved:
                Shipping();
                break;

            // customer received
            case OrderShopStatusEnum.Shipping:
                Received();
                break;

            // customer returned
            case OrderShopStatusEnum.Received:
                Returning();
                break;

            // shop received
            case OrderShopStatusEnum.Returning:
                Returned();
                break;

            // shop checked ok
            case OrderShopStatusEnum.Returned:
                WaitingForRefund();
                break;

            // admin refunded
            case OrderShopStatusEnum.WaitingForRefund:
                Completed();
                break;
            default:
                throw new AppException(ResponseCodeConstants.BAD_REQUEST, "Invalid status", StatusCodes.Status400BadRequest);
        }

        AddOrderTracking();
        return orderShop.Status.ToString();
    }

    public void Reject()
    {
        checkStatus(OrderShopStatusEnum.Pending, OrderShopStatusEnum.Rejected);

        orderShop.Status = OrderShopStatusEnum.Rejected;
        AddOrderTracking();
    }

    public void Cancel()
    {
        checkStatus(OrderShopStatusEnum.WaitForPayment, OrderShopStatusEnum.Cancelled);

        orderShop.Status = OrderShopStatusEnum.Cancelled;
        AddOrderTracking();
    }

    private void ReturnedWithDamage()
    {
        checkStatus(OrderShopStatusEnum.Returned, OrderShopStatusEnum.ReturnedWithDamage);

        orderShop.Status = OrderShopStatusEnum.ReturnedWithDamage;
        AddOrderTracking();
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

    private void checkStatus(OrderShopStatusEnum statusCurrent, OrderShopStatusEnum statusTarget)
    {
        if (orderShop.Status != statusCurrent)
            throw new AppException(ResponseCodeConstants.BAD_REQUEST,
                               ResponseMessageConstrantsOrder.STATUS_CHANGE_NOTALLOWED + 
                               $" ,Status [{statusCurrent}] Required before to become [{statusTarget}]", 
                               StatusCodes.Status400BadRequest);
    }
}