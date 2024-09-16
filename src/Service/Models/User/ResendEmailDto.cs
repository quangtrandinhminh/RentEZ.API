using System.ComponentModel.DataAnnotations;

namespace Service.Models.User;

public class ResendEmailDto
{
    [Required]
    public string UserName { get; set; }
}