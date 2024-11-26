using Repository.Models;
using Service.Helpers;

namespace Service.BusinessModels;

public class CartShop()
{
    public int ShopId { get; set; }
    public string ShopName { get; set; }
    public int? VoucherId { get; set; }
    public decimal TaxRate { get; private set; }
    public decimal Tax { get; private set; }
    public decimal CommissionRate { get; private set; }
    public decimal Commission { get; private set; }
    public decimal ShipFee { get; set; }
    public decimal ShipFeeSupport { get; set; } = 0;
    public decimal TotalRentPrice { get; private set; }
    public decimal PlatformRentPriceDiscount { get; set; } = 0;
    public decimal ShopRentPriceDiscount { get; set; } = 0;
    public decimal TotalDeposit { get; private set; }
    public decimal TotalDiscount { get; private set; }
    public decimal Total { get; private set; }
    public Voucher? ShopVoucher { get; set; }
    public IList<CartItem> OrderDetails { get; set; }

    public void CalculateShopTotal(decimal commissionRate, decimal taxRate, decimal shipFee)
    {
        TotalRentPrice = OrderDetails.Sum(x => x.SubTotalRentPrice);
        TotalDeposit = OrderDetails.Sum(x => x.SubTotalDeposit);
        TotalDiscount = OrderDetails.Sum(x => x.Discount);

        ShipFee = shipFee;
        TaxRate = taxRate;
        CommissionRate = commissionRate;
        Tax = CalculateHelper.CalculateTax(TotalRentPrice, TaxRate);
        Commission = CalculateHelper.CalculateCommission(TotalRentPrice, CommissionRate);

        if (ShopVoucher != null)
        {
            ShopRentPriceDiscount = CalculateHelper.ApplyShopRentVoucher(ShopVoucher, TotalRentPrice);
        }

        TotalDiscount += ShopRentPriceDiscount;
        Total = CalculateHelper.CalculateTotal(TotalRentPrice, TotalDeposit, ShipFee, TotalDiscount);
    }

    public void CalculateShopTotalWithPlatform()
    {
        // platform rent price discount and ship fee support are applied by platform
        TotalDiscount += PlatformRentPriceDiscount;
        TotalDiscount += ShipFeeSupport;

        // calculate total value for the shop
        Total = CalculateHelper.CalculateTotal(TotalRentPrice, TotalDeposit, ShipFee, TotalDiscount);
    }
}