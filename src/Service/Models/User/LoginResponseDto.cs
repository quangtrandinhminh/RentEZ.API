namespace Service.Models.User
{
    public class LoginResponseDto : UserResponseDto
    {
        public string? Token { get; set; }
        public string? RefreshToken { get; set; }
        public DateTimeOffset? RefreshTokenExpiredTime { get; set; }
    }
}
