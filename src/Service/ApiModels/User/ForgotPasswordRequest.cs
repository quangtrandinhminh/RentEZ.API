using System.ComponentModel.DataAnnotations;

namespace Service.Models.User;

public class ForgotPasswordRequest
{
    [Required]
    public string UserName { get; set; }
}