using System.ComponentModel.DataAnnotations;

namespace Service.ApiModels.Order;

public class CartItemCalcRequest
{
    [Range(1, int.MaxValue)]
    public int ProductId { get; set; }

    [Range(1, int.MaxValue)]
    public int RentQuantity { get; set; }

    public DateTimeOffset RentDateTime { get; set; }
    public DateTimeOffset ReturnDateTime { get; set; }
}