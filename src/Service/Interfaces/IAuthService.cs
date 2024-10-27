using Service.ApiModels;
using Service.ApiModels.User;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<IList<RoleResponse>> GetAllRoles();
        Task Register(RegisterRequest request, CancellationToken cancellationToken = default);
        Task RegisterByAdmin(RegisterRequest request, int role);
        Task<LoginResponse> Authenticate(LoginRequest dto);
        Task<LoginResponse> RefreshToken(string token);
        Task VerifyEmail(VerifyEmailRequest request, CancellationToken cancellationToken = default);
        Task ForgotPassword(ForgotPasswordRequest model, CancellationToken cancellationToken = default);
        Task ResetPassword(ResetPasswordRequest dto, CancellationToken cancellationToken = default);
        Task ChangePassword(ChangePasswordRequest dto, CancellationToken cancellationToken = default);
        Task ReSendEmail(ResendEmailRequest model, CancellationToken cancellationToken = default);
        Task<LoginResponse> GoogleAuthenticate(GoogleLoginModel model);
        Task RegisterAsAShopkeeper(RegisterRequest request, CancellationToken cancellationToken = default);
    }
}