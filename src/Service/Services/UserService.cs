using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Repository.Models.Identity;
using Serilog;
using Service.ApiModels.User;
using Service.Interfaces;
using Service.Mapper;
using System.Linq.Expressions;
using Service.ApiModels.PaginatedList;
using Service.Helpers;
using Utility.Constants;
using Utility.Enum;
using Utility.Exceptions;
using Utility.Helpers;
using Microsoft.AspNetCore.Identity;
using Repository.Infrastructure;

namespace Service.Services;

public class UserService(IServiceProvider serviceProvider) : IUserService
{
    private readonly IUserRepository _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
    private readonly IShopRepository _shopRepository = serviceProvider.GetRequiredService<IShopRepository>();
    private readonly MapperlyMapper _mapper = serviceProvider.GetRequiredService<MapperlyMapper>();
    private readonly ILogger _logger = serviceProvider.GetRequiredService<ILogger>();
    private readonly UserManager<UserEntity> _userManager = serviceProvider.GetRequiredService<UserManager<UserEntity>>();
    private readonly IUnitOfWork _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();

    public async Task<PaginatedListResponse<UserResponse>> GetAllUsersAsync(UserRoleEnum? role, int pageNumber, int pageSize)
    {
        _logger.Information($"Get all users by role {role.ToString()}");
        Expression<Func<UserEntity, bool>> predicate = v => v.DeletedTime == null;
        if (role != null)
        {
            predicate = predicate.And(x => x.UserRoles.Any(y => y.Role.Name == role.ToString()));
        }
        var users = await _userRepository.GetAllPaginatedQueryable(
            pageNumber, pageSize,
            predicate, null, e => e.UserRoles);

        return _mapper.Map(users); 
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

        await _userRepository.UpdateAsync(user);
        await _unitOfWork.SaveChangeAsync();
    }

    public async Task<UserResponse> GetByIdAsync(int id)
    {
        _logger.Information($"Get user by id {id}");
        var user = await _userRepository.GetSingleAsync(e => e.Id == id, 
            x => x.UserRoles,
            x => x.ManagedShop
            );
        if (user == null)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstantsUser.USER_NOT_FOUND, StatusCodes.Status404NotFound);
        }

        var response = _mapper.MapToUserResponse(user);
        var roles = await _userManager.GetRolesAsync(user);
        response.Role = roles;
        response.ShopId = user.ManagedShop?.Id;
        return response;
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