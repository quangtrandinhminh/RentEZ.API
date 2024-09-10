using BusinessObject.DTO;
using BusinessObject.DTO.RefreshToken;
using BusinessObject.DTO.Shopkeeper;
using BusinessObject.DTO.User;
using BusinessObject.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.RateLimiting;
using Microsoft.OpenApi.Extensions;
using Service.Interfaces;
using Service.Services;
using System.Data;
using System.Security.Claims;
using Utility.Constants;
using Utility.Enum;
using Utility.Helpers;

namespace RentEZ.WebAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableRateLimiting("EndpointRateLimitPolicy")]
    public class AuthController : ControllerBase
    {
        private readonly IAuthService _authService;
        private readonly IUserService _userService;

        public AuthController(IAuthService authSevices, IUserService userService)
        {
            _userService = userService;
            _authService = authSevices;
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("register")]
        public async Task<IActionResult> Register([FromBody] RegisterDto request)
        {
            await _authService.Register(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.REGIST_USER_SUCCESS));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("admin/roles/all")]
        public async Task<IActionResult> GetAllRoles()
        {
            var roles = await _authService.GetAllRoles();
            return Ok(BaseResponseDto.OkResponseDto(roles));
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        [Route("admin/users/all")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserRole? role, [FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            var users = await _userService.GetAllUsersAsync(role, pageNumber, pageSize);
            return Ok(BaseResponseDto.OkResponseDto(users));
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [Route("admin/register")]
        public async Task<IActionResult> RegisterByAdmin([FromBody] RegisterDto request, int role)
        {
            await _authService.RegisterByAdmin(request, role);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.REGIST_USER_SUCCESS));
        }

        [HttpPost]
        [AllowAnonymous]
        [Route("authenticate")]
        public async Task<IActionResult> Login(LoginDto request)
        {
            return Ok(await _authService.Authenticate(request));
        }

        // google login
        /*[HttpPost]
        [AllowAnonymous]
        [Route("google-login")]
        public async Task<IActionResult> GoogleLogin(GoogleLoginDto request)
        {
            return Ok(await _authService.GoogleLogin(request));
        }*/

        [AllowAnonymous]
        [HttpPost("refresh-token")]
        public async Task<ActionResult> RefreshToken(RefreshTokenDto request)
        {
            var refreshToken = request.Token ?? Request.Cookies["refreshToken"];
            var response = await _authService.RefreshToken(refreshToken);
            SetTokenCookie(response.RefreshToken);
            return Ok(BaseResponseDto.OkResponseDto(response));
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

        [HttpPost("email/verify")]
        public async Task<IActionResult> VerifyEmail(VerifyEmailDto request)
        {
            await _authService.VerifyEmail(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.VERIFY_EMAIL_SUCCESS));
        }

        [HttpPost("password/forgot")]
        public async Task<IActionResult> ForgotPassword(ForgotPasswordDto request)
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
        public async Task<IActionResult> ResetPassword(ResetPasswordDto request)
        {
            await _authService.ResetPassword(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.RESET_PASSWORD_SUCCESS));
        }

        [HttpPost("email/resend")]
        public async Task<IActionResult> ResendEmail(ResendEmailDto request)
        {
            await _authService.ReSendEmail(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.RESEND_EMAIL_SUCCESS));
        }

        [HttpPost("authenticate/google")]
        public async Task<IActionResult> GoogleLogin([FromBody] GoogleLoginModel request)
        {
            return Ok(await _authService.GoogleAuthenticate(request));
        }

        //[HttpPost]
        //[AllowAnonymous]
        //[Route("shopkeeper-register")]
        //public async Task<IActionResult> RegisterAsAShopkeeper([FromBody] RegisterDto request)
        //{
        //    await _authService.RegisterAsAShopkeeper(request);
        //    return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.REGIST_USER_SUCCESS));
        //}

        [HttpGet]
        [AllowAnonymous]
        [Route("get-pending-shopkeeper-list")]
        public async Task<IActionResult> LoadPendingShopkeeperListAsync()
        {
            var pendingShopkeepers = await _userService.GetPendingShopkeepersAsync();
            return Ok(BaseResponseDto.OkResponseDto(pendingShopkeepers));
        }

        [HttpPost]
        [Route("shopkeeper-register")]
        public async Task<IActionResult> RegisterShopkeeper(ShopkeeperRegisterRequestDto request)
        {
            await _authService.RegisterAsAShopkeeper(request);
            return Ok(BaseResponseDto.OkResponseDto(ResponseMessageIdentitySuccess.REGIST_USER_SUCCESS));
        }
    }
}