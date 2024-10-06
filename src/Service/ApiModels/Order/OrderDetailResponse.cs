using Service.ApiModels.Voucher;

namespace Service.ApiModels.Order;

public class OrderDetailResponse : OrderDetailListResponse
{
    public decimal Price { get; set; }
    public decimal RentPrice { get; set; }
    public decimal Deposit { get; set; }
    public decimal DepositRate { get; set; }
    public VoucherResponse? Voucher { get; set; }
    public OrderResponse Order { get; set; }
}