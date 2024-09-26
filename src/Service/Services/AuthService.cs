using System.Security.Claims;
using System.Security.Cryptography;
using System.Text.RegularExpressions;
using Google.Apis.Auth;
using Invedia.Core.StringUtils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Infrastructure;
using Repository.Interfaces;
using Repository.Models.Identity;
using Serilog;
using Service.Interfaces;
using Service.Models;
using Service.Models.User;
using Service.Utils;
using Utility.Config;
using Utility.Constants;
using Utility.Enum;
using Utility.Exceptions;
using Utility.Helpers;
using MapperlyMapper = Service.Mapper.MapperlyMapper;

namespace Service.Services
{
    public class AuthService(IServiceProvider serviceProvider) : IAuthService
    {
        private readonly IUserRepository _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
        private readonly IShopRepository _shopRepository = serviceProvider.GetRequiredService<IShopRepository>();
        private readonly MapperlyMapper _mapper = serviceProvider.GetRequiredService<MapperlyMapper>();
        private readonly RoleManager<RoleEntity> _roleManager = serviceProvider.GetRequiredService<RoleManager<RoleEntity>>();
        private readonly UserManager<UserEntity> _userManager = serviceProvider.GetRequiredService<UserManager<UserEntity>>();
        private readonly ILogger _logger = Log.Logger;
        private readonly IRefreshTokenRepository _refreshTokenRepository = serviceProvider.GetRequiredService<IRefreshTokenRepository>();
        private readonly IEmailService _emailService = serviceProvider.GetRequiredService<IEmailService>();
        private readonly IUnitOfWork _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

        // get all roles
        public async Task<IList<RoleResponseDto>> GetAllRoles()
        {
            _logger.Information("Get all roles");
            var roles = await _roleManager.Roles.ToListAsync();
            return _mapper.Map(roles);
        }

        public async Task Register(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            _logger.Information("Register new user: {@request}", request);
            // get user by name
            var validateUser = await _userManager.FindByNameAsync(request.UserName);
            if (validateUser != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageIdentity.EXISTED_USER, StatusCodes.Status400BadRequest);
            }

            var existingUserWithEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingUserWithEmail != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageIdentity.EXISTED_EMAIL, StatusCodes.Status400BadRequest);
            }

            var existingUserWithPhone = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
            if (existingUserWithPhone != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageIdentity.EXISTED_PHONE, StatusCodes.Status400BadRequest);
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber) && !Regex.IsMatch(request.PhoneNumber, @"^\d{10}$"))
            {
                throw new AppException(ResponseCodeConstants.INVALID_INPUT, ResponseMessageIdentity.PHONENUMBER_INVALID, StatusCodes.Status400BadRequest);
            }

            if (request.Password != request.ConfirmPassword)
            {
                throw new AppException(ResponseCodeConstants.INVALID_INPUT, ResponseMessageIdentity.PASSWORD_NOT_MATCH, StatusCodes.Status400BadRequest);
            }

            try
            {
                var account = _mapper.Map(request);
                account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                account.SecurityStamp = Guid.NewGuid().ToString();
                account.OTP = GenerateOTP();
                await _userRepository.CreateAsync(account, cancellationToken);

                await _userRepository.SaveChangeAsync();
                await _userManager.AddToRoleAsync(account, UserRoleEnum.Customer.ToString());

                var mailRequest = new SendMailModel()
                {
                    Name = account.NormalizedUserName,
                    Email = account.Email,
                    Token = account.OTP,
                    Type = MailTypeEnum.Verify
                };
                _emailService.SendMail(mailRequest);
            }
            catch (Exception e)
            {
                throw new AppException(ResponseCodeConstants.FAILED, e.Message, StatusCodes.Status400BadRequest);
            }
        }

        public async Task RegisterAsAShopkeeper(RegisterRequest request, CancellationToken cancellationToken = default)
        {
            _logger.Information("Register new shop owner: {@request}", request);
            // get user by name
            var validateUser = await _userManager.FindByNameAsync(request.UserName);
            if (validateUser != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageIdentity.EXISTED_USER, StatusCodes.Status400BadRequest);
            }

            var existingUserWithEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingUserWithEmail != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageIdentity.EXISTED_EMAIL, StatusCodes.Status400BadRequest);
            }

            var existingUserWithPhone = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
            if (existingUserWithPhone != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageIdentity.EXISTED_PHONE, StatusCodes.Status400BadRequest);
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber) && !Regex.IsMatch(request.PhoneNumber, @"^\d{10}$"))
            {
                throw new AppException(ResponseCodeConstants.INVALID_INPUT, ResponseMessageIdentity.PHONENUMBER_INVALID, StatusCodes.Status400BadRequest);
            }

            if (request.Password != request.ConfirmPassword)
            {
                throw new AppException(ResponseCodeConstants.INVALID_INPUT, ResponseMessageIdentity.PASSWORD_NOT_MATCH, StatusCodes.Status400BadRequest);
            }
            try
            {
                var account = _mapper.Map(request);
                account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                account.SecurityStamp = Guid.NewGuid().ToString();
                account.OTP = GenerateOTP();
                await _userRepository.CreateAsync(account, cancellationToken);

                await _userRepository.SaveChangeAsync();
                await _userManager.AddToRoleAsync(account, UserRoleEnum.ShopOwner.ToString());

                var mailRequest = new SendMailModel()
                {
                    Name = account.NormalizedUserName,
                    Email = account.Email,
                    Token = account.OTP,
                    Type = MailTypeEnum.Verify
                };
                _emailService.SendMail(mailRequest);
            }
            catch (Exception e)
            {
                throw new AppException(ResponseCodeConstants.FAILED, e.Message, StatusCodes.Status400BadRequest);
            }
        }
        
        // register by admin
        public async Task RegisterByAdmin(RegisterRequest request, int role)
        {
            _logger.Information("Register new user by admin: {@request}", request);
            // check role is valid in system
            var roleEntity = await _roleManager.FindByIdAsync(role.ToString());
            if (roleEntity == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageIdentity.ROLE_INVALID, StatusCodes.Status400BadRequest);
            }

            // get user by name
            var validateUser = await _userManager.FindByNameAsync(request.UserName);
            if (validateUser != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageIdentity.EXISTED_USER, StatusCodes.Status400BadRequest);
            }

            var existingUserWithEmail = await _userManager.FindByEmailAsync(request.Email);
            if (existingUserWithEmail != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageIdentity.EXISTED_EMAIL, StatusCodes.Status400BadRequest);
            }

            var existingUserWithPhone = await _userManager.Users.FirstOrDefaultAsync(x => x.PhoneNumber == request.PhoneNumber);
            if (existingUserWithPhone != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageIdentity.EXISTED_PHONE, StatusCodes.Status400BadRequest);
            }

            if (!string.IsNullOrEmpty(request.PhoneNumber) && !Regex.IsMatch(request.PhoneNumber, @"^\d{10}$"))
            {
                throw new AppException(ResponseCodeConstants.INVALID_INPUT, ResponseMessageIdentity.PHONENUMBER_INVALID, StatusCodes.Status400BadRequest);
            }

            if (request.Password != request.ConfirmPassword)
            {
                throw new AppException(ResponseCodeConstants.INVALID_INPUT, ResponseMessageIdentity.PASSWORD_NOT_MATCH, StatusCodes.Status400BadRequest);
            }

            try
            {
                var account = _mapper.Map(request);
                account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(request.Password);
                account.SecurityStamp = Guid.NewGuid().ToString();
                account.Verified = CoreHelper.SystemTimeNow;
                await _userRepository.CreateAsync(account);
                await _userRepository.SaveChangeAsync();
                await _userManager.AddToRoleAsync(account, roleEntity.NormalizedName);
            }
            catch (Exception e)
            {
                throw new AppException(ResponseCodeConstants.FAILED, e.Message, StatusCodes.Status400BadRequest);
            }
        }

        public async Task<LoginResponseDto> Authenticate(LoginDto dto)
        {
            _logger.Information("Authenticate user: {@dto}", dto);
            var account = await GetUserByUserName(dto.Username);
            if (account == null || account.DeletedTime != null)
                throw new AppException(ErrorCode.UserInvalid, ResponseMessageIdentity.INVALID_USER, StatusCodes.Status401Unauthorized);

            if (!account.IsActive)
                throw new AppException(ErrorCode.UserInActive, ResponseMessageIdentity.EMAIL_VALIDATION_REQUIRED, StatusCodes.Status401Unauthorized);

            // check password
            if (!BCrypt.Net.BCrypt.Verify(dto.Password, account.PasswordHash))
                throw new AppException(ErrorCode.UserPasswordWrong, ResponseMessageIdentity.PASSWORD_WRONG, StatusCodes.Status401Unauthorized);

            try
            {
                var roles = await _userManager.GetRolesAsync(account);
                var token = await GenerateJwtToken(account, roles, 24);
                var refreshToken = GenerateRefreshToken(account.Id, 48);
                RemoveOldRefreshTokens(account.RefreshTokens);
                await _refreshTokenRepository.AddAsync(refreshToken);
                var count = await _unitOfWork.SaveChangeAsync();

                var response = _mapper.Map(account);
                response.Token = token;
                response.RefreshToken = refreshToken.Token;
                response.RefreshTokenExpiredTime = refreshToken.Expires;
                response.Role = roles;
                return response;
            }
            catch (Exception e)
            {
                throw new AppException(ResponseCodeConstants.FAILED, e.Message, StatusCodes.Status400BadRequest);
            }
        }

        public async Task<LoginResponseDto> GoogleAuthenticate(GoogleLoginModel model)
        {
            _logger.Information("Google authenticate: {@model}", model);
            var payload = await ValidateGoogleToken(model.IdToken);
            if (payload == null)
            {
                throw new AppException(ErrorCode.TokenInvalid, ResponseMessageIdentity.GOOGLE_TOKEN_INVALID, StatusCodes.Status401Unauthorized);
            }

            var account = await GetUserByEmail(payload.Email);
            if (account == null)
            {
                account = new UserEntity
                {
                    UserName = payload.Email,
                    Email = payload.Email,
                    FullName = payload.Name,
                    Avatar = payload.Picture,
                    EmailConfirmed = true,
                    SecurityStamp = Guid.NewGuid().ToString(),
                    Verified = CoreHelper.SystemTimeNow
                };
                await _userRepository.CreateAsync(account);
                await _userRepository.SaveChangeAsync();
                await _userManager.AddToRoleAsync(account, UserRoleEnum.Customer.ToString());
            }

            var roles = await _userManager.GetRolesAsync(account);
            var token = await GenerateJwtToken(account, roles, 24);
            var refreshToken = GenerateRefreshToken(account.Id, 48);
            RemoveOldRefreshTokens(account.RefreshTokens);
            await _refreshTokenRepository.AddAsync(refreshToken);
            await _unitOfWork.SaveChangeAsync();

            var response = _mapper.Map(account);
            response.Token = token;
            response.RefreshToken = refreshToken.Token;
            response.RefreshTokenExpiredTime = refreshToken.Expires;
            response.Role = roles;
            return response;
        }

        public async Task<LoginResponseDto> RefreshToken(string token)
        {
            _logger.Information("Refresh token: {@token}", token);
            var (refreshToken, account) = await GetRefreshToken(token);
            refreshToken.Expires = CoreHelper.SystemTimeNow;
            _refreshTokenRepository.Update(refreshToken);
            var newRefreshToken = GenerateRefreshToken(account.Id, 48);

            newRefreshToken.UserId = account.Id;
            await _refreshTokenRepository.AddAsync(newRefreshToken);
            
            RemoveOldRefreshTokens(account.RefreshTokens);
            var count = await _unitOfWork.SaveChangeAsync();

            try
            {
                var roles = await _userManager.GetRolesAsync(account);
                var jwtToken = await GenerateJwtToken(account, roles, 24);
                var response = _mapper.Map(account);
                response.Token = jwtToken;
                response.RefreshToken = newRefreshToken.Token;
                response.RefreshTokenExpiredTime = refreshToken.Expires;
                response.Role = roles;
                return response;
            }
            catch (Exception e)
            {
                throw new AppException(ResponseCodeConstants.FAILED, e.Message, StatusCodes.Status400BadRequest);
            }
        }

        public async Task VerifyEmail(VerifyEmailDto dto, CancellationToken cancellationToken = default)
        {
            _logger.Information("Verify email: {@dto}", dto);
            var account = await GetUserByUserName(dto.UserName);

            if (account == null || account.OTP != dto.OTP)
                throw new AppException(ErrorCode.TokenInvalid, ResponseMessageIdentity.TOKEN_INVALID, StatusCodes.Status401Unauthorized);

            account.Verified = CoreHelper.SystemTimeNow;
            account.OTP = null;
            await _userRepository.UpdateAsync(account, cancellationToken);
            await _userRepository.SaveChangeAsync();
        }

        /// <summary>
        /// Send mail to user to reset password
        /// </summary>
        /// <param name="model"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task ForgotPassword(ForgotPasswordDto model, CancellationToken cancellationToken = default)
        {
            _logger.Information("Forgot password: {@model}", model);
            var account = await GetUserByUserName(model.UserName);
            if (account == null) throw new AppException(ErrorCode.UserInvalid, ResponseMessageIdentity.INVALID_USER, StatusCodes.Status401Unauthorized);

            // create reset token that expires after 1 day
            account.OTP = GenerateOTP();
            await _userRepository.UpdateAsync(account, cancellationToken);

            var mailRequest = new SendMailModel
            {
                Name = account.NormalizedUserName,
                Email = account.Email,
                Token = account.OTP,
                Type = MailTypeEnum.ResetPassword
            };
            _emailService.SendMail(mailRequest);
        }

        /// <summary>
        /// Reset password for user after verify OTP
        /// </summary>
        /// <param name="dto"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        /// <exception cref="AppException"></exception>
        public async Task ResetPassword(ResetPasswordDto dto, CancellationToken cancellationToken = default)
        {
            _logger.Information("Reset password: {@dto}", dto);
            var account = await GetUserByUserName(dto.UserName);

            if (account == null) throw new AppException(ErrorCode.UserInvalid, ResponseMessageIdentity.INVALID_USER, StatusCodes.Status401Unauthorized);

            if (account.OTP != dto.OTP)
            {
                throw new AppException(ErrorCode.TokenInvalid, ResponseMessageIdentity.TOKEN_INVALID, StatusCodes.Status401Unauthorized);
            }

            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            account.OTP = null;
            account.AccessFailedCount = 0;
            account.LastUpdatedTime = CoreHelper.SystemTimeNow;
            account.LastUpdatedBy = account.Id;

            await _userRepository.UpdateAsync(account, cancellationToken);
        }

        public async Task ChangePassword(ChangePasswordDto dto, CancellationToken cancellationToken)
        {
            _logger.Information("Change password: {@dto}", dto);
            var account = await GetUserByUserName(dto.UserName);

            if (account == null || !account.IsActive) throw new AppException(ErrorCode.UserInvalid, ResponseMessageIdentity.INVALID_USER, StatusCodes.Status401Unauthorized);

            if (!BCrypt.Net.BCrypt.Verify(dto.OldPassword, account.PasswordHash))
            {
                throw new AppException(ErrorCode.UserPasswordWrong, ResponseMessageIdentity.UNAUTHENTICATED, StatusCodes.Status401Unauthorized);
            }

            account.PasswordHash = BCrypt.Net.BCrypt.HashPassword(dto.Password);
            account.LastUpdatedTime = CoreHelper.SystemTimeNow;
            account.LastUpdatedBy = account.Id;

            await _userRepository.UpdateAsync(account, cancellationToken);
        }

        public async Task ReSendEmail(ResendEmailDto model, CancellationToken cancellationToken = default)
        {
            _logger.Information("Resend email: {@model}", model);
            var account = await GetUserByUserName(model.UserName);
            if (account == null) throw new AppException(ErrorCode.UserInvalid, ResponseMessageIdentity.INVALID_USER, StatusCodes.Status400BadRequest);
            if (account.OTP == null) throw new AppException(ErrorCode.Validated, ResponseMessageIdentity.EMAIL_VALIDATED, StatusCodes.Status400BadRequest);

            account.OTP = GenerateOTP();
            await _userRepository.UpdateAsync(account, cancellationToken);

            var mailRequest = new SendMailModel()
            {
                Name = account.NormalizedUserName,
                Email = account.Email,
                Token = account.OTP,
                Type = MailTypeEnum.Verify
            };
            _emailService.SendMail(mailRequest);
        }

        private async Task<string> GenerateJwtToken(UserEntity loggedUser, IList<string> roles, int hour)
        {
            var claims = new List<Claim>();
            claims.AddRange(await _userManager.GetClaimsAsync(loggedUser));
            // Add role claims
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));

                // Use RoleManager to find the role and add its claims
                var roleEntity = await _roleManager.FindByNameAsync(role);
                if (roleEntity != null)
                {
                    var roleClaims = await _roleManager.GetClaimsAsync(roleEntity);
                    claims.AddRange(roleClaims);
                }
            }

            claims.AddRange(new[]
            {
                new Claim(ClaimTypes.Sid, loggedUser.Id.ToString()),
                new Claim("UserName", loggedUser.UserName ?? string.Empty),
                new Claim(ClaimTypes.Name, loggedUser.FullName ?? string.Empty),
                new Claim(ClaimTypes.Email, loggedUser.Email ?? string.Empty),
                new Claim(ClaimTypes.MobilePhone, loggedUser.PhoneNumber ?? string.Empty),
                new Claim(ClaimTypes.Expired, CoreHelper.SystemTimeNow.AddHours(hour).Date.ToShortDateString())
            });

            return JwtUtils.GenerateToken(claims.Distinct(), hour);
        }

        private static RefreshToken GenerateRefreshToken(int userId, int hour)
        {
            var randomByte = new byte[64];
            var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            rngCryptoServiceProvider.GetBytes(randomByte);
            var refreshToken = new RefreshToken
            {
                UserId = userId,
                Token = Convert.ToBase64String(randomByte),
                Expires = CoreHelper.SystemTimeNow.AddHours(hour),
            };
            return refreshToken;
        }

        /// <summary>
        /// Remove old refresh token from database which is inactive and expired more than 2 days
        /// </summary>
        /// <param name="refreshTokens"></param>
        private void RemoveOldRefreshTokens(ICollection<RefreshToken> refreshTokens)
        {
            var removeList = refreshTokens.Where(x => !x.IsActive
                                                      && x.CreatedTime.AddDays(2) <= CoreHelper.SystemTimeNow).ToList();
            if (removeList.Any())
            {
                _refreshTokenRepository.DeleteRange(removeList);
            }
        }

        private async Task<(RefreshToken, UserEntity)> GetRefreshToken(string token)
        {
            var account = await _userRepository.GetSingleAsync(y
                                => y.RefreshTokens.Any(t => t.Token == token)
                            , _ => _.RefreshTokens);
            if (account == null || account.DeletedTime != null)
            {
                throw new AppException(ErrorCode.TokenInvalid, ResponseMessageIdentity.TOKEN_INVALID, StatusCodes.Status401Unauthorized);
            }

            var refreshToken = account.RefreshTokens.Single(x => x.Token == token);
            if (!refreshToken.IsActive)
            {
                throw new AppException(ErrorCode.TokenExpired, ResponseMessageIdentity.TOKEN_INVALID, StatusCodes.Status401Unauthorized);
            }

            return (refreshToken, account);
        }

        private async Task<UserEntity?> GetUserByUserName(string userName, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetSingleAsync(_ => _.UserName == userName, x => x.RefreshTokens);

            if (user != null && user.DeletedTime != null)
            {
                user = null;
            }

            return user;
        }

        private async Task<UserEntity?> GetUserByEmail(string email, CancellationToken cancellationToken = default)
        {
            var user = await _userRepository.GetSingleAsync(_ => _.Email == email, x => x.RefreshTokens);

            if (user != null && user.DeletedTime != null)
            {
                user = null;
            }

            return user;
        }

        private string GenerateOTP()
        {
            var otp = StringHelper.Generate(6, false, false, true, false);
            return otp;
        }

        private async Task<GoogleJsonWebSignature.Payload?> ValidateGoogleToken(string idToken)
        {
            try
            {
                var settings = new GoogleJsonWebSignature.ValidationSettings()
                {
                    Audience = new List<string>() { GoogleSetting.Instance.ClientID }
                };

                var payload = await GoogleJsonWebSignature.ValidateAsync(idToken, settings);
                return payload;
            }
            catch (Exception e)
            {
                _logger.Error(e, "An error occurred while validating Google token");
            }

            return null;
        }
    }
}