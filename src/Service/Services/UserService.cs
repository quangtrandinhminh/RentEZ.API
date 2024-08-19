using BusinessObject.DTO.User;
using BusinessObject.Entities.Identity;
using BusinessObject.Mapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Repository.Interfaces;
using Serilog;
using Service.Interfaces;
using System.Text.RegularExpressions;
using Utility.Constants;
using Utility.Enum;
using Utility.Exceptions;

namespace Service.Services;

public class UserService(IServiceProvider serviceProvider) : IUserService
{
    private readonly IUserRepository _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
    private readonly MapperlyMapper _mapper = serviceProvider.GetRequiredService<MapperlyMapper>();
    private readonly RoleManager<RoleEntity> _roleManager = serviceProvider.GetRequiredService<RoleManager<RoleEntity>>();
    private readonly UserManager<UserEntity> _userManager = serviceProvider.GetRequiredService<UserManager<UserEntity>>();
    private readonly ILogger _logger = Log.Logger;
    private readonly SignInManager<UserEntity> _signInManager = serviceProvider.GetRequiredService<SignInManager<UserEntity>>();

    public async Task<IList<UserResponseDto>> GetAllUsersByRoleAsync(UserRole role)
    {
        switch (role)
        {
            case UserRole.Admin:
                return await GetAdminsAsync();
            case UserRole.Customer:
                return await GetCustomersAsync();
            default:
                throw new AppException(ResponseCodeConstants.INVALID_INPUT, ResponseMessageIdentity.ROLE_INVALID, StatusCodes.Status400BadRequest);
        }
    }

    private async Task<IList<UserResponseDto>> GetAdminsAsync()
    {
        var admins = await _userManager.GetUsersInRoleAsync(UserRole.Admin.ToString());

        if (admins == null || admins.Count == 0)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstantsUser.ADMIN_NOT_FOUND, StatusCodes.Status404NotFound);
        }

        var response = _mapper.Map(admins);

        foreach (var vet in response)
        {
            vet.Role = UserRole.Admin.ToString();
        }

        return response;
    }

    public async Task<IList<UserResponseDto>> GetCustomersAsync()
    {
        var customers = await _userManager.GetUsersInRoleAsync(UserRole.Customer.ToString());

        if (customers == null || customers.Count == 0)
        {
            throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstantsUser.CUSTOMER_NOT_FOUND, StatusCodes.Status404NotFound);
        }

        var response = _mapper.Map(customers);

        foreach (var vet in response)
        {
            vet.Role = UserRole.Customer.ToString();
        }

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