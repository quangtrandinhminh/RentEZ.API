using Repository.Extensions;
using Service.ApiModels.User;
using Utility.Enum;

namespace Service.Interfaces;

public interface IUserService
{
    Task<PaginatedList<UserResponse>> GetAllUsersAsync(UserRoleEnum? role, int pageNumber, int pageSize);
    Task CreateUserAsync(UserCreateRequest dto);
    Task UpdateUserAsync(UserUpdateRequest dto);
    Task<UserResponse> GetByIdAsync(int id);
    Task DeleteUserAsync(int id);
}