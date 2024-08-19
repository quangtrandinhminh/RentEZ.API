using BusinessObject.DTO.User;
using Utility.Enum;

namespace Service.Interfaces;

public interface IUserService
{
    Task<IList<UserResponseDto>> GetAllUsersByRoleAsync(UserRole role);
    Task CreateUserAsync(UserCreateRequestDto dto);
    Task UpdateUserAsync(UserUpdateRequestDto dto);
    Task<UserResponseDto> GetByIdAsync(int id);
    Task DeleteUserAsync(int id);
}