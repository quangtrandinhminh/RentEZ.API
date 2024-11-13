using Repository.Extensions;
using Service.ApiModels.PaginatedList;
using Service.ApiModels.User;
using Utility.Enum;

namespace Service.Interfaces;

public interface IUserService
{
    Task<PaginatedListResponse<UserResponse>> GetAllUsersAsync(UserRoleEnum? role, int pageNumber, int pageSize);
    Task CreateUserAsync(UserCreateRequest request);
    Task UpdateUserAsync(UserUpdateRequest request);
    Task<UserResponse> GetByIdAsync(int id);
    Task DeleteUserAsync(int id);
}