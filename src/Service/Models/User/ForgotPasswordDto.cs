using System.ComponentModel.DataAnnotations;

namespace Service.Models.User;

public class ForgotPasswordDto
{
    [Required]
    public string UserName { get; set; }
}