using Repository.Extensions;
using Service.Models.User;
using Utility.Enum;

namespace Service.Interfaces;

public interface IUserService
{
    Task<PaginatedList<UserResponseDto>> GetAllUsersAsync(UserRole? role, int pageNumber, int pageSize);
    Task CreateUserAsync(UserCreateRequestDto dto);
    Task UpdateUserAsync(UserUpdateRequestDto dto);
    Task<UserResponseDto> GetByIdAsync(int id);
    Task DeleteUserAsync(int id);
}