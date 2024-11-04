using Repository.Models;
using Service.BusinessModels;
using Utility.Enum;

namespace Service.Helpers
{
    public static class CalculateHelper
    {
        public static decimal CalculateTotalRentPrice(decimal rentPrice, int quantity)
        {
            var totalRentPrice = rentPrice * quantity;
            return totalRentPrice;
        }

        public static decimal CalculateTax(decimal totalRentPrice, decimal taxRate)
        {
            var tax = totalRentPrice * taxRate;
            return tax;
        }

        public static decimal CalculateTotal(decimal totalRentPrice, decimal totalDeposit, decimal totalShipFee, decimal totalDiscount)
        {
            var total = totalRentPrice + totalDeposit + totalShipFee - totalDiscount;
            return total;
        }

        public static decimal CalculateCommission(decimal rentPrice, decimal commission)
        {
            var totalPlatformFee = rentPrice * commission;
            return totalPlatformFee;
        }

        public static decimal ApplyPlatformRentVoucherAmount(Voucher voucher, decimal totalRentPrice)
        {
            if (voucher.MinRentValue.HasValue && totalRentPrice < voucher.MinRentValue.Value) return 0;

            var discount = ApplyRentValueDiscount(voucher, totalRentPrice);
            return discount;
        }

        public static decimal ApplyPlatformShipVoucherAmount(Voucher voucher, decimal totalShipFee)
        {
            var discount = ApplyShipValueDiscount(voucher, totalShipFee);
            return discount;
        }

        public static void ApplyPlatformRentVoucherPercent(Voucher voucher, decimal totalRentPrice, CartShop shop)
        {
            if (voucher.MinRentValue.HasValue && totalRentPrice < voucher.MinRentValue.Value) return;
            shop.PlatformRentPriceDiscount = ApplyRentPercentDiscount(voucher, shop.TotalRentPrice);
        }

        public static decimal ApplyShopRentVoucher(Voucher voucher, decimal totalRentPrice)
        {
            if (voucher.Type != VoucherTypeEnum.Rent) return 0;
            if (voucher.MinRentValue.HasValue && totalRentPrice < voucher.MinRentValue.Value) return 0;

            // 1 shop apply 1 voucher only, by amount or percent
            var discount = 0m;
            switch (voucher.ValueType)
            {
                case VoucherValueTypeEnum.Amount:
                    discount = ApplyRentValueDiscount(voucher, totalRentPrice);
                    break;

                case VoucherValueTypeEnum.Percent:
                    discount = ApplyRentPercentDiscount(voucher, totalRentPrice);
                    break;
            }

            return discount;
        }

        public static decimal ApplyPlatformShipVoucher(Voucher voucher, decimal totalRentPrice,
            decimal totalShipFee)
        {
            if (voucher.Type != VoucherTypeEnum.Ship) return 0;
            if (voucher.MinRentValue.HasValue && totalRentPrice < voucher.MinRentValue.Value) return 0;

            var discount = 0m;
            switch (voucher.ValueType)
            {
                // Apply the ship discount amount on platform ship fee
                case VoucherValueTypeEnum.Amount:
                    discount = ApplyShipValueDiscount(voucher, totalShipFee);
                    break;

                // Apply the ship discount % on shop ship fee to all shops
                case VoucherValueTypeEnum.Percent:
                    discount = ApplyShipPercentDiscount(voucher, totalShipFee);
                    break;
            }

            return discount;
        }

        private static decimal ApplyRentValueDiscount(Voucher voucher, decimal totalDiscount)
        {
            var discount = Math.Min(voucher.Value, totalDiscount);
            return discount;
        }

        private static decimal ApplyRentPercentDiscount(Voucher voucher, decimal rentPrice)
        {
            decimal discount = rentPrice * voucher.Value;

            if (voucher.MaxDiscountValue.HasValue)
            {
                discount = Math.Min(discount, voucher.MaxDiscountValue.Value);
            }

            return discount;
        }

        private static decimal ApplyShipValueDiscount(Voucher voucher, decimal totalShipFee)
        {
            var discount = Math.Min(voucher.Value, totalShipFee);
            return discount;
        }

        private static decimal ApplyShipPercentDiscount(Voucher voucher, decimal shipFee)
        {
            decimal discount = shipFee * voucher.Value;

            if (voucher.MaxDiscountValue.HasValue)
            {
                discount = Math.Min(discount, voucher.MaxDiscountValue.Value);
            }

            return discount;
        }
    }
}