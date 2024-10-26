/*using Repository.Models;
using Service.Helpers;
using Utility.Enum;

namespace TestProject
{
    public class VoucherApplyTest
    {
        // Không giảm giá thuê cho đơn hàng có giá trị thuê < 150
        [Fact]
        public void ApplyVoucher_ShouldNotApplyRentValueDiscount_WhenRentPriceBelowMinimumValue()
        {
            // Arrange
            decimal rentPrice = 100m;
            decimal totalDiscount = 0m;
            decimal shipFee = 50m;

            var vouchers = new List<Voucher>
            {
                new Voucher { Type = VoucherTypeEnum.RentValue, MinRentValue = 150m, Value = 20m }
            };

            // Act
            CalculateHelper.ApplyVoucher(vouchers, ref rentPrice, ref shipFee, ref totalDiscount);

            // Assert
            Assert.Equal(100m, rentPrice);  // Rent price unchanged
            Assert.Equal(0m, totalDiscount);  // No discount applied
        }

        // Giảm giá 20 cho đơn hàng có giá trị thuê >= 150
        [Fact]
        public void ApplyVoucher_ShouldApplyRentValueDiscount_WhenRentPriceMeetsMinimumValue()
        {
            // Arrange
            decimal rentPrice = 200m;
            decimal totalDiscount = 0m;
            decimal shipFee = 50m;

            var vouchers = new List<Voucher>
            {
                new Voucher { Type = VoucherTypeEnum.RentValue, MinRentValue = 150m, Value = 20m }
            };

            // Act
            CalculateHelper.ApplyVoucher(vouchers, ref rentPrice, ref shipFee, ref totalDiscount);

            // Assert
            Assert.Equal(180m, rentPrice);  // 200 - 20 = 180
            Assert.Equal(20m, totalDiscount);  // Total discount is 20
        }

        // Giảm 10% giá thuê cho đơn hàng
        [Fact]
        public void ApplyVoucher_ShouldApplyRentPercentDiscount_WhenNoMinValueOrMaxValue()
        {
            // Arrange
            decimal rentPrice = 200m;
            decimal totalDiscount = 0m;
            decimal shipFee = 50m;

            var vouchers = new List<Voucher>
            {
                new Voucher { Type = VoucherTypeEnum.RentPercent, Value = 0.10m } // 10% discount
            };

            // Act
            CalculateHelper.ApplyVoucher(vouchers, ref rentPrice, ref shipFee, ref totalDiscount);

            // Assert
            Assert.Equal(180m, rentPrice);  // 10% of 200 = 20, so rentPrice = 200 - 20 = 180
            Assert.Equal(20m, totalDiscount);  // Total discount = 20
        }

        // Giảm 20% giá thuê cho đơn hàng có giá trị thuê >= 150, tối đa 30
        [Fact]
        public void ApplyVoucher_ShouldApplyRentPercentDiscount_WithMaxValue()
        {
            // Arrange
            decimal rentPrice = 200m;
            decimal totalDiscount = 0m;
            decimal shipFee = 50m;

            var vouchers = new List<Voucher>
            {
                new Voucher { Type = VoucherTypeEnum.RentPercent, Value = 0.20m, MaxDiscountValue = 30m } // 20% discount with max 30
            };

            // Act
            CalculateHelper.ApplyVoucher(vouchers, ref rentPrice, ref shipFee, ref totalDiscount);

            // Assert
            Assert.Equal(170m, rentPrice);  // 20% of 200 = 40, but max is 30, so rentPrice = 200 - 30 = 170
            Assert.Equal(30m, totalDiscount);  // Total discount capped at 30
        }

        // Giảm 15 phí ship cho đơn hàng có giá trị thuê >= 150
        [Fact]
        public void ApplyVoucher_ShouldApplyShipValueDiscount_WhenRentPriceMeetsMinimumValue()
        {
            // Arrange
            decimal rentPrice = 200m;
            decimal totalDiscount = 0m;
            decimal shipFee = 50m;

            var vouchers = new List<Voucher>
            {
                new Voucher { Type = VoucherTypeEnum.ShipValue, MinRentValue = 150m, Value = 15m }
            };

            // Act
            CalculateHelper.ApplyVoucher(vouchers, ref rentPrice, ref shipFee, ref totalDiscount);

            // Assert
            Assert.Equal(35m, shipFee);  // 50 - 15 = 35
            Assert.Equal(15m, totalDiscount);  // Total discount is 15
        }

        // Giảm 30% cho phí ship, tối đa 10
        [Fact]
        public void ApplyVoucher_ShouldApplyShipPercentDiscount_WithMaxValue()
        {
            // Arrange
            decimal rentPrice = 200m;
            decimal totalDiscount = 0m;
            decimal shipFee = 50m;

            var vouchers = new List<Voucher>
            {
                new Voucher { Type = VoucherTypeEnum.ShipPercent, Value = 0.30m, MaxDiscountValue = 10m } // 30% discount, max 10
            };

            // Act
            CalculateHelper.ApplyVoucher(vouchers, ref rentPrice, ref shipFee, ref totalDiscount);

            // Assert
            Assert.Equal(40m, shipFee);  // 30% of 50 = 15, but max is 10, so shipFee = 50 - 10 = 40
            Assert.Equal(10m, totalDiscount);  // Total discount is capped at 10
        }

        // Giảm 20% cho phí ship
        [Fact]
        public void ApplyVoucher_ShouldApplyShipPercentDiscount_WhenNoMinMaxValue()
        {
            // Arrange
            decimal rentPrice = 200m;
            decimal totalDiscount = 0m;
            decimal shipFee = 50m;

            var vouchers = new List<Voucher>
            {
                new Voucher { Type = VoucherTypeEnum.ShipPercent, Value = 0.20m } // 20% discount
            };

            // Act
            CalculateHelper.ApplyVoucher(vouchers, ref rentPrice, ref shipFee, ref totalDiscount);

            // Assert
            Assert.Equal(40m, shipFee);  // 20% of 50 = 10, so shipFee = 50 - 10 = 40
            Assert.Equal(10m, totalDiscount);  // Total discount = 10
        }

        // Không giảm ship cho đơn hàng có giá trị thuê < 150
        [Fact]
        public void ApplyVoucher_ShouldNotApplyShipValueDiscount_WhenRentPriceBelowMinimumValue()
        {
            // Arrange
            decimal rentPrice = 100m;
            decimal totalDiscount = 0m;
            decimal shipFee = 50m;

            var vouchers = new List<Voucher>
            {
                new Voucher { Type = VoucherTypeEnum.ShipValue, MinRentValue = 150m, Value = 15m }
            };

            // Act
            CalculateHelper.ApplyVoucher(vouchers, ref rentPrice, ref shipFee, ref totalDiscount);

            // Assert
            Assert.Equal(50m, shipFee);  // Ship fee remains unchanged
            Assert.Equal(0m, totalDiscount);  // No discount applied
        }
    }
}*/