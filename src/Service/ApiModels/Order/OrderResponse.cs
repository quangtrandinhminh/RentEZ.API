using Service.ApiModels.Voucher;

namespace Service.ApiModels.Order;

public class OrderResponse : OrderListResponse
{
    public string OrderNumber { get; set; }
    public string CustomerEmail { get; set; }
    public string CustomerAddress { get; set; }
    public string CustomerNote { get; set; }
    public decimal SubTotalRentPrice { get; set; }
    public decimal SubTotalDeposit { get; set; }
    public decimal Discount { get; set; }
    public VoucherResponse? Voucher { get; set; }
    public IList<OrderDetailListResponse> OrderDetails { get; set; } = new List<OrderDetailListResponse>();
}