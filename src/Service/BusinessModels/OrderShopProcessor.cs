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
    }

    public void Reject()
    {
        if (orderShop.Status != OrderShopStatusEnum.Pending)
            throw new AppException(ResponseCodeConstants.BAD_REQUEST,
                ResponseMessageConstrantsOrder.STATUS_CHANGE_NOTALLOWED, StatusCodes.Status400BadRequest);

        orderShop.Status = OrderShopStatusEnum.Rejected;
        AddOrderTracking();
    }

    public void Cancel()
    {
        if (orderShop.Status != OrderShopStatusEnum.WaitForPayment)
            throw new AppException(ResponseCodeConstants.BAD_REQUEST,
                ResponseMessageConstrantsOrder.STATUS_CHANGE_NOTALLOWED, StatusCodes.Status400BadRequest);

        orderShop.Status = OrderShopStatusEnum.Cancelled;
        AddOrderTracking();
    }

    private void ReturnedWithDamage()
    {
        if (orderShop.Status != OrderShopStatusEnum.Returned)
            throw new AppException(ResponseCodeConstants.BAD_REQUEST,
                ResponseMessageConstrantsOrder.STATUS_CHANGE_NOTALLOWED, StatusCodes.Status400BadRequest);

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
}