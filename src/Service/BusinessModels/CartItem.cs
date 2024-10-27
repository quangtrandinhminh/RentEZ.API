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
    public decimal RentPrice { get; set; }
    public decimal Deposit { get; set; }
    public decimal DepositRate { get; set; }
    public decimal SubTotalRentPrice { get; private set; }
    public decimal SubTotalDeposit { get; private set; }
    public decimal Discount { get; private set; } = 0;
    public decimal SubTotal { get; private set; }

    public void CalculateSubTotal()
    {
        SubTotalRentPrice = CalculateHelper.CalculateTotalRentPrice(RentPrice, RentQuantity, (int)(ReturnDateTime - RentDateTime).TotalDays);
        SubTotalDeposit = Deposit * RentQuantity; 
        SubTotal = SubTotalRentPrice + SubTotalDeposit - Discount;
    }
}