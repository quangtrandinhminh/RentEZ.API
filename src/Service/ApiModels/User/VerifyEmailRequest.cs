namespace Service.Models.User;

public class VerifyEmailRequest
{
    public string OTP { get; set; }
    public string UserName { get; set; }
}