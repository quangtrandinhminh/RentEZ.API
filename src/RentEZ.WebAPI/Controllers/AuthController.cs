using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Service.Interfaces;
using Service.Models;
using Service.Models.RefreshToken;
using Service.Models.User;
using Utility.Constants;
using Utility.Enum;

namespace RentEZ.WebAPI.Controllers
{
    [ApiController]
    [Route("api/auth")]
    [EnableRateLimiting("EndpointRateLimitPolicy")]
    public class AuthController(IServiceProvider serviceProvider) : ControllerBase
    {
        private readonly IAuthService _authService = serviceProvider.GetRequiredService<IAuthService>();
        private readonly IUserService _userService = serviceProvider.GetRequiredService<IUserService>();

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterRequest request)
        {
            await _authService.Register(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.REGIST_USER_SUCCESS));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("admin/roles")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _authService.GetAllRoles();
            return Ok(BaseResponseDto.OkResponseDto(roles));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("admin/users")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserRoleEnum? role, [FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            var users = await _userService.GetAllUsersAsync(role, pageNumber, pageSize);
            return Ok(BaseResponseDto.OkResponseDto(users));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("admin/register")]
        public async Task<IActionResult> RegisterByAdmin([FromBody] RegisterRequest request, int role)
        {
            await _authService.RegisterByAdmin(request, role);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.REGIST_USER_SUCCESS));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("authentication")]
        public async Task<IActionResult> Login(LoginRequest request)
        {
            return Created(nameof(Login), await _authService.Authenticate(request));
        }

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken(RefreshToken request)
        {
            var refreshToken = request.Token ?? Request.Cookies["refreshToken"];
            var response = await _authService.RefreshToken(refreshToken);
            SetTokenCookie(response.RefreshToken);
            return Created(nameof(RefreshToken), BaseResponseDto.OkResponseDto(response));
        }

        private void SetTokenCookie(string token)
        {
            var cookieOptions = new CookieOptions
            {
                HttpOnly = true,
                Expires = DateTime.UtcNow.AddDays(7)
            };
            Response.Cookies.Append("refreshToken", token, cookieOptions);
        }

        [HttpPost("email/verification")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailRequest request)
        {
            await _authService.VerifyEmail(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.VERIFY_EMAIL_SUCCESS));
        }

        [HttpPost("password/forgot")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordRequest request)
        {
            await _authService.ForgotPassword(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.FORGOT_PASSWORD_SUCCESS));
        }

        [HttpPost("password/change")]
        public async Task<IActionResult> ChangePassword(ChangePasswordDto request)
        {
            await _authService.ChangePassword(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.CHANGE_PASSWORD_SUCCESS));
        }

        [HttpPost("password/reset")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest request)
        {
            await _authService.ResetPassword(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.RESET_PASSWORD_SUCCESS));
        }

        [HttpPost("email/resend")]
        public async Task<IActionResult> ResendEmail(ResendEmailRequest request)
        {
            await _authService.ReSendEmail(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.RESEND_EMAIL_SUCCESS));
        }

        [HttpPost("authentication/google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginModel request)
        {
            return Created(nameof(GoogleLogin), await _authService.GoogleAuthenticate(request));
        }

        [HttpPost]
        [Route("register/shopkeeper")]
        public async Task<IActionResult> RegisterShopkeeper([FromBody] RegisterRequest request)
        {
            await _authService.RegisterAsAShopkeeper(request);
            return Created(nameof(RegisterShopkeeper), BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.REGIST_USER_SUCCESS));
        }
    }
}