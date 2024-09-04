using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BusinessObject.DTO.User;
using BusinessObject.Entities.Identity;
using BusinessObject.Models;
using Microsoft.AspNetCore.Identity;

namespace Service.Interfaces
{
    public interface IAuthService
    {
        Task<IList<RoleResponseDto>> GetAllRoles();
        Task Register(RegisterDto dto, CancellationToken cancellationToken = default);
        Task RegisterByAdmin(RegisterDto dto, int role);
        Task<LoginResponseDto> Authenticate(LoginDto dto);
        Task<LoginResponseDto> RefreshToken(string token);
        Task VerifyEmail(VerifyEmailDto dto, CancellationToken cancellationToken = default);
        Task ForgotPassword(ForgotPasswordDto model, CancellationToken cancellationToken = default);
        Task ResetPassword(ResetPasswordDto dto, CancellationToken cancellationToken = default);
        Task ChangePassword(ChangePasswordDto dto, CancellationToken cancellationToken = default);
        Task ReSendEmail(ResendEmailDto model, CancellationToken cancellationToken = default);
        Task<LoginResponseDto> GoogleAuthenticate(GoogleLoginModel model);
    }
}