using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository.Extensions;
using Repository.Interfaces;
using Repository.Models.Identity;
using Serilog;
using Service.ApiModels.User;
using Service.Interfaces;
using Service.Mapper;
using Utility.Constants;
using Utility.Enum;
using Utility.Exceptions;
using Utility.Helpers;

namespace Service.Services;

public class UserService(IServiceProvider serviceProvider) : IUserService
{
    private readonly IUserRepository _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
    private readonly IShopRepository _shopRepository = serviceProvider.GetRequiredService<IShopRepository>();
    private readonly MapperlyMapper _mapper = serviceProvider.GetRequiredService<MapperlyMapper>();
    private readonly ILogger _logger = Log.Logger;


    public async Task<PaginatedList<UserResponse>> GetAllUsersAsync(UserRoleEnum? role, int pageNumber, int pageSize)
    {
        _logger.Information($"Get all users by role {role.ToString()}");
        var users = _userRepository.GetAllWithCondition(
            x => x.DeletedTime == null, x => x.UserRoles);
        if (role != null)
        {
            users = users.Where(x => x.UserRoles.Any(y => y.Role.Name == role.ToString()));
        }

        if (users.IsNullOrEmpty())
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstantsUser.USER_NOT_FOUND, StatusCodes.Status404NotFound);
        }

        var userResponses = _mapper.Map(users);
        var response = await PaginatedList<UserResponse>.CreateAsync(userResponses, pageNumber, pageSize);
        return response;
    }

    public async Task CreateUserAsync(UserCreateRequest request)
    {
        _logger.Information("Create user {@request}", request);
        var user = _mapper.Map(request);
        await _userRepository.CreateAsync(user);
    }

    public async Task UpdateUserAsync(UserUpdateRequest request)
    {
        _logger.Information("Update user {@request}", request);
        var user = await GetUserById(request.Id);
        _mapper.Map(request, user);
    }

    public async Task<UserResponse> GetByIdAsync(int id)
    {
        _logger.Information($"Get user by id {id}");
        var user = await GetUserById(id);
        return _mapper.Map(user);
    }

    public async Task DeleteUserAsync(int id)
    {
        _logger.Information($"Delete user by id {id}");
        var user = await GetUserById(id);
        user.DeletedTime = CoreHelper.SystemTimeNow;
        await _userRepository.UpdateAsync(user);
    }

    private async Task<UserEntity> GetUserById(int id)
    {
        var user = await _userRepository.GetSingleAsync(e => e.Id == id);
        if (user == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstantsUser.USER_NOT_FOUND, StatusCodes.Status404NotFound);
        }

        return user;
    }
}