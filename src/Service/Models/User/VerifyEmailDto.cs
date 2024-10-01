namespace Service.Models.User;

public class VerifyEmailDto
{
    public string OTP { get; set; }
    public string UserName { get; set; }
}