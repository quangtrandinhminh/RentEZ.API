using Repository.Models;
using Utility.Enum;
using CalculateHelper = Service.Helpers.CalculateHelper;

namespace Service.BusinessModels;

public class Cart()
{
    public decimal TotalRentPrice { get; private set; }
    public decimal TotalDeposit { get; private set; }
    public decimal CommissionRate { get; private set; }
    public decimal TotalCommissionFee { get; private set; }
    public decimal TotalShipFee { get; set; }
    public decimal ShipSupportFee { get; private set; } = 0;
    public decimal ShopRentDiscount { get; private set; }
    public decimal PlatformRentDiscount { get; private set; } = 0;
    public decimal TaxRate { get; private set; }
    public decimal Tax { get; private set; }
    public decimal TotalDiscount { get; private set; }
    public decimal Total { get; private set; }
    public List<Voucher> PlatformVouchers { get; set; } = new List<Voucher>();
    public IList<CartShop> OrderShops { get; set; } = new List<CartShop>();

    public void CalculateTotal(decimal taxRate, decimal platformRate, IList<CartShop> cartShops)
    {
        // Calculate total values for the cart
        OrderShops = cartShops;
        TotalRentPrice = OrderShops.Sum(x => x.TotalRentPrice);
        TotalDeposit = OrderShops.Sum(x => x.TotalDeposit);
        ShopRentDiscount = OrderShops.Sum(x => x.TotalDiscount);
        TotalShipFee = OrderShops.Sum(x => x.ShipFee);

        // Calculate tax, commission, and total values for the cart
        TaxRate = taxRate;
        Tax = CalculateHelper.CalculateTax(TotalRentPrice, TaxRate);
        
        CommissionRate = platformRate;
        TotalCommissionFee = CalculateHelper.CalculateCommission(TotalRentPrice, CommissionRate);

        // Apply vouchers to the cart
        var shipVoucher = PlatformVouchers.FirstOrDefault(x => x.Type == VoucherTypeEnum.Ship);
        var platformVoucher = PlatformVouchers.FirstOrDefault(x => x.Type == VoucherTypeEnum.Rent);

        if (platformVoucher != null)
            PlatformRentDiscount += CalculateHelper.ApplyShopRentVoucher(platformVoucher, TotalRentPrice);
        if (shipVoucher != null)
            ShipSupportFee = CalculateHelper.ApplyPlatformShipVoucher(shipVoucher, TotalRentPrice, TotalShipFee);

        /*if (platformVoucher is { ValueType: VoucherValueTypeEnum.Amount })
            PlatformRentDiscount = CalculateHelper.ApplyPlatformRentVoucherAmount(platformVoucher, TotalRentPrice);

        if (shipVoucher is { ValueType: VoucherValueTypeEnum.Amount })
            ShipSupportFee = CalculateHelper.ApplyPlatformShipVoucherAmount(shipVoucher, TotalShipFee);

        if (platformVoucher is { ValueType: VoucherValueTypeEnum.Percent } ||
            shipVoucher is { ValueType: VoucherValueTypeEnum.Percent })
        {
            var shipFeeSupport = ShipSupportFee;
            foreach (var shop in OrderShops)
            {
                if (shipVoucher is { ValueType: VoucherValueTypeEnum.Percent })
                {
                    CalculateHelper.ApplyPlatformShipVoucher(shipVoucher, shop, TotalRentPrice, TotalShipFee, ref shipFeeSupport);
                    ShipSupportFee = shipFeeSupport;
                }

                if (platformVoucher is { ValueType: VoucherValueTypeEnum.Percent })
                    CalculateHelper.ApplyPlatformRentVoucherPercent(platformVoucher, TotalRentPrice, shop);
                shop.CalculateShopTotalWithPlatform();
            }
        }*/

        // Calculate the total value for the cart
        // ShopRentDiscount and PlatformRentDiscount are calculated in the CalculateShopTotalWithPlatform method
        PlatformRentDiscount = OrderShops.Sum(x => x.PlatformRentPriceDiscount);
        TotalDiscount = ShopRentDiscount + PlatformRentDiscount + ShipSupportFee;
        Total = CalculateHelper.CalculateTotal(TotalRentPrice, TotalDeposit, TotalShipFee, TotalDiscount);
    }
}