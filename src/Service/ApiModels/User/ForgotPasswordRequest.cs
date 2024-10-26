using System.ComponentModel.DataAnnotations;

namespace Service.ApiModels.User;

public class ForgotPasswordRequest
{
    [Required]
    public string UserName { get; set; }
}