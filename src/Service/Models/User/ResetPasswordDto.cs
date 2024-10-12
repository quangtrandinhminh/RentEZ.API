using System.ComponentModel.DataAnnotations;
using Utility.Constants;

namespace Service.Models.User;

public class ResetPasswordDto
{
    [Required]
    public string OTP { get; set; }

    [Required]
    [DataType(DataType.Password)]
    [MinLength(8)]
    public string Password { get; set; }

    [Required]
    [Compare(nameof(Password), ErrorMessage = ResponseMessageIdentity.PASSWORD_NOT_MATCH)]
    public string ConfirmPassword { get; set; }

    [Required]
    public string UserName { get; set; }
}