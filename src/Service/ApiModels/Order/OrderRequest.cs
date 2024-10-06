using System.ComponentModel.DataAnnotations;
using Utility.Enum;

namespace Service.ApiModels.Order;

public class OrderRequest : OrderCalcRequest
{
    public int CustomerId { get; set; }
    public string OrderAddress { get; set; }
    public decimal TotalRentPrice { get; set; }
    public decimal TotalDeposit { get; set; }
    public decimal ShipFee { get; set; }
    public decimal PlatformRate { get; set; }
    public decimal PlatformFee { get; set; }
    public decimal ShipSupportFee { get; set; }
    public decimal Tax { get; set; }
    public decimal TaxRate { get; set; }
    public decimal Total { get; set; }
    public PaymentMethodEnum? PaymentMethod { get; set; }
    public string? Note { get; set; }
}