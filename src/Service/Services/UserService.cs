using BusinessObject.DTO.User;
using BusinessObject.Entities.Identity;
using BusinessObject.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using Repository.Extensions;
using Repository.Interfaces;
using Serilog;
using Service.Interfaces;
using Utility.Constants;
using Utility.Enum;
using Utility.Exceptions;

namespace Service.Services;

public class UserService(IServiceProvider serviceProvider) : IUserService
{
    private readonly IUserRepository _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
    private readonly MapperlyMapper _mapper = serviceProvider.GetRequiredService<MapperlyMapper>();
    private readonly ILogger _logger = Log.Logger;


    public async Task<PaginatedList<UserResponseDto>> GetAllUsersAsync(UserRole? role, int pageNumber, int pageSize)
    {
        _logger.Information($"Get all users by role {role.ToString()}");
        var users = _userRepository.GetAllWithCondition(
            x => x.IsActive, x => x.UserRoles);
        if (role != null)
        {
            users = users.Where(x => x.UserRoles.Any(y => y.Role.Name == role.ToString()));
        }

        if (users.IsNullOrEmpty())
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstantsUser.USER_NOT_FOUND, StatusCodes.Status404NotFound);
        }

        var userResponses = _mapper.Map(users);
        var response = await PaginatedList<UserResponseDto>.CreateAsync(userResponses, pageNumber, pageSize);
        return response;
    }

    public Task CreateUserAsync(UserCreateRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public Task UpdateUserAsync(UserUpdateRequestDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<UserResponseDto> GetByIdAsync(int id)
    {
        var user = await _userRepository.GetSingleAsync(e => e.Id == id);

        return _mapper.UserToUserResponseDto(user);
    }

    public Task DeleteUserAsync(int id)
    {
        throw new NotImplementedException();
    }
}