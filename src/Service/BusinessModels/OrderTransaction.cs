using Repository.Models;
using Utility.Enum;
using Utility.Helpers;

namespace Service.BusinessModels;

public class OrderTransaction(
    decimal platformRate,
    decimal taxRate,
    decimal shipFee,
    decimal shipSupportFee,
    IList<OrderDetail> orderDetails,
    Voucher voucher)
{
    public decimal TotalRentPrice { get; set; }
    public decimal TotalDeposit { get; set; }
    public decimal ShipFee { get; set; } = shipFee;
    public decimal PlatformRate { get; set; } = platformRate;
    public decimal PlatformFee { get; set; }
    public decimal ShipSupportFee { get; set; } = shipSupportFee;
    public decimal Tax { get; private set; }
    public decimal TaxRate { get; set; } = taxRate;
    public decimal Total { get; set; }
    public PaymentStatusEnum PaymentStatus { get; set; } = PaymentStatusEnum.Pending;
    public IList<OrderDetail> OrderDetails { get; set; } = orderDetails;
    public Voucher Voucher { get; set; } = voucher;

    public void CalculateTotal()
    {
        var calculateHelper = new CalculateHelper();
        TotalRentPrice = OrderDetails.Sum(x => x.SubTotalRentPrice);
        TotalDeposit = OrderDetails.Sum(x => x.SubTotalDeposit);
        Tax = calculateHelper.CalculateTax(TotalRentPrice, TaxRate);
        PlatformFee = calculateHelper.CalculateTotalPlatformFee(TotalRentPrice, PlatformRate);
        Total = calculateHelper.CalculateTotal(TotalRentPrice, TotalDeposit, ShipFee, PlatformFee, ShipSupportFee, Tax);
    }

    private void ApplyVoucher()
    {
        if (Voucher == null)
        {
            return;
        }

        switch (Voucher.Type)
        {
            case VoucherTypeEnum.RentValue when Voucher.MinValue.HasValue && TotalRentPrice < Voucher.MinValue.Value:
                return;
            case VoucherTypeEnum.RentValue:
                TotalRentPrice -= Voucher.Value;
                break;
            case VoucherTypeEnum.RentPercent when Voucher.MaxValue.HasValue:
                var discount = TotalRentPrice * Voucher.Value;
                TotalRentPrice = (decimal)(discount > Voucher.MaxValue.Value ? TotalRentPrice - Voucher.MaxValue : TotalRentPrice - discount);
                break;
            case VoucherTypeEnum.RentPercent:
                TotalRentPrice -= TotalRentPrice * Voucher.Value;
                break;
        }
    }
}