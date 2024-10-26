using System.ComponentModel.DataAnnotations;

namespace Service.ApiModels.User;

public class LoginRequest
{
    [Required]
    public string Username { get; set; }
    [Required]
    public string Password { get; set; }
}