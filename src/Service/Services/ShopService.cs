using Repository.Interfaces;
using Serilog;
using Service.Interfaces;
using Microsoft.EntityFrameworkCore;
using Repository.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Utility.Constants;
using Utility.Exceptions;
using Microsoft.IdentityModel.Tokens;
using Repository.Models;
using MapperlyMapper = Service.Mapper.MapperlyMapper;
using System.Security.Claims;
using Service.ApiModels.Shop;
using System.Text.Json;
using System.Linq.Expressions;
using Service.Helpers;
using Service.ApiModels.PaginatedList;
using Microsoft.AspNetCore.Identity;
using Repository.Models.Identity;
using Repository.Repositories;

namespace Service.Services
{
    public class ShopService(IServiceProvider serviceProvider) : IShopService
    {
        private readonly IShopRepository _shopRepository = serviceProvider.GetRequiredService<IShopRepository>();
        private readonly IUserRepository _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
        private readonly MapperlyMapper _mapper = serviceProvider.GetRequiredService<MapperlyMapper>();
        private readonly RoleManager<RoleEntity> _roleManager = serviceProvider.GetRequiredService<RoleManager<RoleEntity>>();
        private readonly UserManager<UserEntity> _userManager = serviceProvider.GetRequiredService<UserManager<UserEntity>>();
        private readonly ILogger _logger = serviceProvider.GetRequiredService<ILogger>();
        private readonly IUnitOfWork _unitOfWork = serviceProvider.GetRequiredService<IUnitOfWork>();
        private readonly IHttpContextAccessor _httpContextAccessor = serviceProvider.GetRequiredService<IHttpContextAccessor>();

        // create new shop
        public async Task CreateShop(ShopCreateRequest shopRequest, CancellationToken cancellationToken = default)
        {
            _logger.Information("Creating new shop");

            var currentOwnerIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid);
            if (currentOwnerIdClaim == null)
            {
                throw new AppException(ResponseCodeConstants.FAILED, ResponseMessageConstrantsShop.UNAUTHORIZED, StatusCodes.Status401Unauthorized);
            }
            var loggedInOwnerId = int.Parse(currentOwnerIdClaim.Value);
            var existShopOwner = await _userRepository.GetSingleAsync(x => x.Id == shopRequest.OwnerId);
            if (existShopOwner == null)
            {
                throw new AppException(ResponseCodeConstants.FAILED, ResponseMessageConstrantsShop.OWNER_NOTFOUND, StatusCodes.Status400BadRequest);
            }
            if (existShopOwner.Verified == null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.IS_INACTIVE_OWNER, StatusCodes.Status400BadRequest);
            }
            if (shopRequest.OwnerId != loggedInOwnerId)
            {
                throw new AppException(ResponseCodeConstants.FAILED, ResponseMessageConstrantsShop.INVALID_OWNER, StatusCodes.Status403Forbidden);
            }
            var existingShopForOwner = await _shopRepository.GetSingleAsync(x => x.OwnerId == loggedInOwnerId);
            if (existingShopForOwner != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.ALREADY_OWNED_ANOTHER_SHOP, StatusCodes.Status400BadRequest);
            }
            var existedShopEmail = await _shopRepository.GetSingleAsync(x => x.ShopEmail == shopRequest.ShopEmail);
            if (existedShopEmail != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.EXISTED_EMAIL, StatusCodes.Status400BadRequest);
            }
            var existShopName = await _shopRepository.GetSingleAsync(x => x.ShopName == shopRequest.ShopName);
            if (existShopName != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.EXISTED_NAME, StatusCodes.Status400BadRequest);
            }
            var existShopAddress = await _shopRepository.GetSingleAsync(x => x.ShopAddress == shopRequest.ShopAddress);
            if (existShopAddress != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.EXISTED_ADDRESS, StatusCodes.Status400BadRequest);
            }
            var existShopPhone = await _shopRepository.GetSingleAsync(x => x.ShopPhone == shopRequest.ShopPhone);
            if (existShopPhone != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.EXISTED_PHONE, StatusCodes.Status400BadRequest);
            }
            var existShopAvatar = await _shopRepository.GetSingleAsync(x => x.ShopAvatar == shopRequest.ShopAvatar);
            if (existShopAvatar != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.EXISTED_AVATAR, StatusCodes.Status400BadRequest);
            }
            var existpReceivingAccount = await _shopRepository.GetSingleAsync(x => x.AccountNo == shopRequest.AccountNo);
            if (existpReceivingAccount != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.EXISTED_Receiving_Account, StatusCodes.Status400BadRequest);
            }
            try
            {
                var newShop = _mapper.MapShopToCreateShop(shopRequest);

                newShop.CreatedTime = DateTimeOffset.UtcNow;
                newShop.LastUpdatedTime = DateTimeOffset.UtcNow;
                newShop.CreatedBy = loggedInOwnerId;

                // Add new shop
                await _shopRepository.AddAsync(newShop, cancellationToken);

                // Update user role to ShopOwner
                var user = await _userManager.FindByIdAsync(shopRequest.OwnerId.ToString());
                if (user == null)
                {
                    throw new AppException(ResponseCodeConstants.FAILED, ResponseMessageConstrantsShop.OWNER_NOTFOUND, StatusCodes.Status400BadRequest);
                }

                var currentRoles = await _userManager.GetRolesAsync(user);
                if (!currentRoles.Contains("ShopOwner"))
                {
                    if (string.IsNullOrEmpty(user.SecurityStamp))
                    {
                        await _userManager.UpdateSecurityStampAsync(user);
                    }

                    // Add ShopOwner role while keeping existing roles
                    var addRoleResult = await _userManager.AddToRoleAsync(user, "ShopOwner");
                    if (!addRoleResult.Succeeded)
                    {
                        throw new AppException(ResponseCodeConstants.FAILED, "Failed to add ShopOwner role", StatusCodes.Status400BadRequest);
                    }
                }
                await _unitOfWork.SaveChangeAsync(_httpContextAccessor);

                _logger.Information($"New shop created with ID: {newShop.Id} and user {user.Id} role updated to ShopOwner successfully");
            }
            catch (Exception ex)
            {
                throw new AppException(ResponseCodeConstants.FAILED, ex.Message, StatusCodes.Status400BadRequest);
            }
        }

        // get all pending shops
        public async Task<List<ShopResponse>> GetPendingShopList()
        {
            _logger.Information($"Get all pending shops");
            var shops = await _shopRepository.GetAllWithCondition().Include(x => x.Owner).ToListAsync();
            var inActiveShop = shops.Where(x => !x.IsActive).ToList();
            if (inActiveShop.IsNullOrEmpty())
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.NO_INISACTIVE_SHOP_FOUND, StatusCodes.Status404NotFound);
            }
            return _mapper.ShopsToShopsResponseDto(inActiveShop).ToList();
        }

        // get shop by Id
        public async Task<ShopResponse> GetShopById(int id)
        {
            _logger.Information($"Get shop by id");
            var shop = await _shopRepository.GetAllWithCondition().Include(s => s.Owner).FirstOrDefaultAsync(x => x.Id == id);

            if (shop == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.NOTFOUND, StatusCodes.Status404NotFound);
            }
            return _mapper.ShopToShopResponseDto(shop);
        }

        // get all shops
        public async Task<PaginatedListResponse<ShopResponse>> GetAllShops(ShopQueryParams queryParams)
        {
            _logger.Information($"Get all shops with params: {JsonSerializer.Serialize(queryParams)}");
            Expression<Func<Shop, bool>> predicate = v => v.DeletedTime == null;
            if (queryParams.IsVerified.HasValue)
            {
                if (queryParams.IsVerified.Value)
                {
                    predicate = predicate.And(x => x.Verified.HasValue);
                }
                else
                {
                    predicate = predicate.And(x => !x.Verified.HasValue);
                }
            }
            if (!string.IsNullOrWhiteSpace(queryParams.SearchTerm))
            {
                var searchTerm = queryParams.SearchTerm.Trim().ToLower();
                predicate = predicate.And(s =>
                    (s.ShopName != null && s.ShopName.ToLower().Contains(searchTerm)) ||
                    (s.ShopEmail != null && s.ShopEmail.ToLower().Contains(searchTerm)) ||
                    (s.ShopPhone != null && s.ShopPhone.Contains(searchTerm)) ||
                    (s.ShopAddress != null && s.ShopAddress.ToLower().Contains(searchTerm)));
            }

            var shops = await _shopRepository.GetAllPaginatedQueryable(
                queryParams.PageNumber,
                queryParams.PageSize,
                predicate,
                s => s.Id,
                s => s.Owner);

            bool hasVerifiedShops = await _shopRepository.GetAllWithCondition(s => s.Verified.HasValue).CountAsync() > 0;
            if (queryParams.IsVerified == false && !shops.Items.Any() && hasVerifiedShops)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.NO_INISACTIVE_SHOP_FOUND, StatusCodes.Status404NotFound);
            }

            if (shops.Items == null || !shops.Items.Any())
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.NOTFOUND, StatusCodes.Status404NotFound);
            }

            var shopsList = shops.Items.ToList();

            return new PaginatedListResponse<ShopResponse>
            {
                Items = (IReadOnlyCollection<ShopResponse>)_mapper.ShopsToShopsResponseDto(shopsList),
                PageNumber = shops.PageNumber,
                TotalPages = shops.TotalPages,
                TotalCount = shops.TotalCount
            };
        }

        // shop approval
        public async Task ShopToApprove(int id)
        {
            _logger.Information($"Shop to approve");
            var shopApproval = _shopRepository.GetById(id);
            if (shopApproval == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.NOTFOUND, StatusCodes.Status404NotFound);
            }
            if (shopApproval.OwnerId == null)
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.OWNER_NOTFOUND, StatusCodes.Status404NotFound);
            }
            if (shopApproval.IsVerified)
            {
                throw new AppException(ResponseCodeConstants.BAD_REQUEST, ResponseMessageConstrantsShop.ALREADY_APROVED, StatusCodes.Status400BadRequest);
            }
            try
            {
                shopApproval.Verified = DateTimeOffset.UtcNow;
                _shopRepository.Update(shopApproval);
                await _unitOfWork.SaveChangeAsync();
            }
            catch(Exception ex)
            {
                throw new AppException(ResponseCodeConstants.FAILED, ex.Message, StatusCodes.Status400BadRequest);
            }
        }

        // update shops
        public async Task UpdateShopAsync(ShopCreateRequest shopRequest, int id)
        {
            _logger.Information("Update shop");
            var currentOwnerIdClaim = _httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.Sid);
            if (currentOwnerIdClaim == null)
            {
                throw new AppException(ResponseCodeConstants.UNAUTHORIZED, ResponseMessageConstrantsShop.UNAUTHORIZED, StatusCodes.Status401Unauthorized);
            }
            var loggedInOwnerId = int.Parse(currentOwnerIdClaim.Value);
            var shop = await _shopRepository.GetSingleAsync(x => x.Id == id);
            if(shop == null)
            {
                throw new AppException(ResponseCodeConstants.UNAUTHORIZED, ResponseMessageConstrantsShop.NOTFOUND, StatusCodes.Status401Unauthorized);
            }
            var existShop = await _shopRepository.GetSingleAsync(x => x.OwnerId == loggedInOwnerId);
            //if (existShop == null)
            //{
            //    throw new AppException(ResponseCodeConstants.UNAUTHORIZED, ResponseMessageConstrantsShop.NOTFOUND, StatusCodes.Status401Unauthorized);
            //}
            var existShopOwner = await _userRepository.GetSingleAsync(x => x.Id == shopRequest.OwnerId);
            if (existShopOwner == null)
            {
                throw new AppException(ResponseCodeConstants.FAILED, ResponseMessageConstrantsShop.OWNER_NOTFOUND, StatusCodes.Status404NotFound);
            }
            if (existShopOwner.Verified == null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.IS_INACTIVE_OWNER, StatusCodes.Status400BadRequest);
            }
            if (existShop.OwnerId != loggedInOwnerId || existShop.Id != id || shopRequest.OwnerId != loggedInOwnerId)
            {
                throw new AppException(ResponseCodeConstants.UNAUTHORIZED, "Bạn không thể cập nhật thông tin cửa hàng của người khác", StatusCodes.Status403Forbidden);
            }
            var existedShopEmail = await _shopRepository.GetSingleAsync(x => x.ShopEmail == shopRequest.ShopEmail && x.Id != id);
            if (existedShopEmail != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.EXISTED_EMAIL, StatusCodes.Status400BadRequest);
            }
            var existShopName = await _shopRepository.GetSingleAsync(x => x.ShopName == shopRequest.ShopName && x.Id != id);
            if (existShopName != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.EXISTED_NAME, StatusCodes.Status400BadRequest);
            }
            var existShopPhone = await _shopRepository.GetSingleAsync(x => x.ShopPhone == shopRequest.ShopPhone && x.Id != id);
            if (existShopPhone != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.EXISTED_PHONE, StatusCodes.Status400BadRequest);
            }
            var existShopAddress = await _shopRepository.GetSingleAsync(x => x.ShopAddress == shopRequest.ShopAddress && x.Id != id);
            if (existShopAddress != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.EXISTED_ADDRESS, StatusCodes.Status400BadRequest);
            }
            var existShopAvatar = await _shopRepository.GetSingleAsync(x => x.ShopAvatar == shopRequest.ShopAvatar && x.Id != id);
            if (existShopAvatar != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.EXISTED_AVATAR, StatusCodes.Status400BadRequest);
            }
            var existpReceivingAccount = await _shopRepository.GetSingleAsync(x => x.AccountNo == shopRequest.AccountNo && x.Id != id);
            if (existpReceivingAccount != null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.EXISTED_Receiving_Account, StatusCodes.Status400BadRequest);
            }
            if(existShop.ShopEmail == shopRequest.ShopEmail &&
               existShop.ShopName == shopRequest.ShopName &&
               existShop.ShopPhone == shopRequest.ShopPhone &&
               existShop.ShopAvatar == shopRequest.ShopAvatar && 
               existShop.ShopAddress == shopRequest.ShopAddress &&
               existShop.AccountNo == shopRequest.AccountNo &&
               existShop.AccountName == shopRequest.AccountName &&
               existShop.BankId == shopRequest.BankId &&
               existShop.Id == id)
            {
                throw new AppException(ResponseCodeConstants.NOTHINGCHANGED, ResponseMessageConstrantsShop.NOTHING_CHANGED, StatusCodes.Status400BadRequest);
            }
            try
            {
                _mapper.ShopToCreateShop(shopRequest, existShop);
                existShop.LastUpdatedTime = DateTimeOffset.UtcNow;

                _shopRepository.Update(existShop);
                await _unitOfWork.SaveChangeAsync();
                _logger.Information("Updated shop successfully");
            }
            catch (Exception ex)
            {
                throw new AppException(ResponseCodeConstants.FAILED, ex.Message, StatusCodes.Status400BadRequest);
            }
        }
    }
}
