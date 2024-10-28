using Service.Helpers;

namespace Service.BusinessModels;

public class CartItem
{
    public int ProductId { get; set; }
    public int ShopId { get; set; }
    public int RentQuantity { get; set; }
    public DateTimeOffset RentDateTime { get; set; }
    public DateTimeOffset ReturnDateTime { get; set; }
    public decimal Price { get; set; }
    public decimal RentPriceOnDays { get; set; }
    public IList<double> RentPrice { get; set; }
    public decimal Deposit { get; set; }
    public decimal DepositRate { get; set; }
    public decimal SubTotalRentPrice { get; private set; }
    public decimal SubTotalDeposit { get; private set; }
    public decimal Discount { get; private set; } = 0;
    public decimal SubTotal { get; private set; }

    public void CalculateSubTotal()
    {
        var totalDays = (int)(ReturnDateTime - RentDateTime).TotalDays;
        if (totalDays < 1)
        {
            totalDays = 1;
        }

        // if totalDays > RentPrice.Count, then calculate RentPriceOnDays by the last element of RentPrice and the first element of RentPrice
        if (totalDays > RentPrice.Count)
        {
            RentPriceOnDays = (decimal)RentPrice.Last() + (decimal)RentPrice.First() *
                (totalDays - RentPrice.Count);
        }
        else
        {
            RentPriceOnDays = (decimal)RentPrice.ElementAt(totalDays - 1);
        }
        
        SubTotalRentPrice = CalculateHelper.CalculateTotalRentPrice(RentPriceOnDays, RentQuantity);
        SubTotalDeposit = Deposit * RentQuantity; 
        SubTotal = SubTotalRentPrice + SubTotalDeposit - Discount;
    }
}