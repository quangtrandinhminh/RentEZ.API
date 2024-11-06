using System.ComponentModel.DataAnnotations;

namespace Service.ApiModels.User;

public class UserResponse
{
    public int Id { get; set; }
    public string? UserName { get; set; }
    public string? Email { get; set; }
    public string? PhoneNumber { get; set; }
    public string? FullName { get; set; } = string.Empty;
    public string? Address { get; set; } = string.Empty;
    public string? Avatar { get; set; } = string.Empty;
    public IList<string?> Role { get; set; } = new List<string?>();
    public DateTimeOffset? BirthDate { get; set; }
    public int? BankId { get; set; }
    public String? AccountNo { get; set; }
    public String? AccountName { get; set; }
}