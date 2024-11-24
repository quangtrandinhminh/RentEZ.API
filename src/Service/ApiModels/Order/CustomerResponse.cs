namespace Service.ApiModels.Order;

public class CustomerResponse
{
    public int Id { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FullName { get; set; } = string.Empty;
}