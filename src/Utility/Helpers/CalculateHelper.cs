using Swashbuckle.Swagger;

namespace Utility.Helpers
{
    public class CalculateHelper
    {
        public decimal CalculateTax(decimal totalRentPrice, decimal taxRate)
        {
            var tax = totalRentPrice * taxRate;
            return tax;
        }

        public decimal CalculateTotal(decimal totalRentPrice, decimal totalDeposit, decimal shipFee,
            decimal totalPlatformFee, decimal shipSupportFee, decimal tax)
        {
            var total = totalRentPrice + totalDeposit + shipFee + totalPlatformFee + shipSupportFee + tax;
            return total;
        }

        public decimal CalculateTotalPlatformFee(decimal totalRentPrice, decimal platformRate)
        {
            var totalPlatformFee = totalRentPrice * platformRate;
            return totalPlatformFee;
        }
    }
}