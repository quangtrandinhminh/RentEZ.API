using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Service.ApiModels.User;
using Service.ApiModels;
using Service.Interfaces;
using Utility.Constants;
using Utility.Enum;

namespace RentEZ.WebAPI.Controllers
{
    [Route("api/users")]
    [ApiController]
    [Authorize]
    public class UserController(IServiceProvider serviceProvider) : ControllerBase
    {
        private readonly IUserService _userService = serviceProvider.GetRequiredService<IUserService>();

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAllUsers([FromQuery] UserRoleEnum? role, [FromQuery] int pageNumber = 1, int pageSize = 10)
        {
            var users = await _userService.GetAllUsersAsync(role, pageNumber, pageSize);
            return Ok(BaseResponse.OkResponseDto(users));
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> GetUserById([FromRoute] int id)
        {
            var user = await _userService.GetByIdAsync(id);
            return Ok(BaseResponse.OkResponseDto(user));
        }

        [HttpPut]
        public async Task<IActionResult> UpdateUser([FromBody] UserUpdateRequest request)
        {
            await _userService.UpdateUserAsync(request);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageIdentitySuccess.UPDATE_USER_SUCCESS));
        }

        [HttpDelete]
        [Route("{id}")]
        public async Task<IActionResult> DeleteUser([FromRoute] int id)
        {
            await _userService.DeleteUserAsync(id);
            return Ok(BaseResponse.OkResponseDto(ResponseMessageIdentitySuccess.DELETE_USER_SUCCESS));
        }
    }
}
