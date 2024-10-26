using System.ComponentModel;

namespace Utility.Enum;

public enum OrderShopStatusEnum
{
    [Description("WaitForPayment")]
    WaitForPayment = 1,

    [Description("Pending")]
    Pending,

    [Description("Approved")]
    Approved,

    [Description("Shipping")]
    Shipping,

    [Description("Received")]
    Received,

    [Description("Returning")]
    Returning,

    [Description("Returned")]
    Returned,

    [Description("Returned with damage")]
    ReturnedWithDamage,

    [Description("Waiting for refund")]
    WaitingForRefund,

    [Description("Completed")]
    Completed,

    [Description("Rejected")]
    Rejected,

    [Description("Cancelled")]
    Cancelled
}