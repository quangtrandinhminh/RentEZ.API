using System.ComponentModel.DataAnnotations;

namespace Service.ApiModels.User;

public class ResendEmailRequest
{
    [Required]
    public string UserName { get; set; }
}