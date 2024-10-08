﻿using Repository.Interfaces;
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
using Service.Models.Shop;
using MapperlyMapper = Service.Mapper.MapperlyMapper;
using System.Security.Claims;

namespace Service.Services
{
    public class ShopService(IServiceProvider serviceProvider) : IShopService
    {
        private readonly IShopRepository _shopRepository = serviceProvider.GetRequiredService<IShopRepository>();
        private readonly IUserRepository _userRepository = serviceProvider.GetRequiredService<IUserRepository>();
        private readonly MapperlyMapper _mapper = serviceProvider.GetRequiredService<MapperlyMapper>();
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
            var existShopOwner = await _userRepository.GetSingleAsync(x => x.Id == shopRequest.OwnerId);
            if (existShopOwner == null)
            {
                throw new AppException(ResponseCodeConstants.FAILED, ResponseMessageConstrantsShop.OWNER_NOTFOUND, StatusCodes.Status400BadRequest);
            }
            if(existShopOwner.Verified == null)
            {
                throw new AppException(ResponseCodeConstants.EXISTED, ResponseMessageConstrantsShop.IS_INACTIVE_OWNER, StatusCodes.Status400BadRequest);
            }

            try
            {
                var newShop = new Shop
                {
                    OwnerId = shopRequest.OwnerId,
                    ShopAddress = shopRequest.ShopAddress,
                    ShopName = shopRequest.ShopName,
                    LastUpdatedTime = DateTimeOffset.UtcNow,
                    CreatedTime = DateTimeOffset.UtcNow,
                    ShopAvatar = shopRequest.ShopAvatar,
                    ShopEmail = shopRequest.ShopEmail,
                    ShopPhone = shopRequest.ShopPhone,
                    CreatedBy = loggedInOwnerId,
                };
                _mapper.ShopToCreateShop(shopRequest, newShop);

                await _shopRepository.AddAsync(newShop, cancellationToken);
                await _unitOfWork.SaveChangeAsync();
                _logger.Information("New shop created successfully");
            }
            catch (Exception ex)
            {
                throw new AppException(ResponseCodeConstants.FAILED, ex.Message, StatusCodes.Status400BadRequest);
            }
        }

        // get all shops
        public async Task<List<ShopResponse>> GetPendingShopList()
        {
            _logger.Information($"Get all pending shops");
            var shops = await _shopRepository.GetAllWithCondition()
                .Include(x => x.Owner)
                .ToListAsync();
            var inActiveShop = shops
                .Where(x => !x.IsActive)
                .ToList();
            if (inActiveShop.IsNullOrEmpty())
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.NO_INISACTIVE_SHOP_FOUND, StatusCodes.Status404NotFound);
            }
            return _mapper.ShopToShopResponseDto(inActiveShop).ToList();
        }

        // get all products
        public async Task<List<ShopResponse>> GetAllShops()
        {
            _logger.Information($"Get all shops");
            var shops = await _shopRepository.GetAllWithCondition().Include(s => s.Owner).ToListAsync();
            if (shops.IsNullOrEmpty())
            {
                throw new AppException(ResponseCodeConstants.NOT_FOUND, ResponseMessageConstrantsShop.NOTFOUND, StatusCodes.Status404NotFound);
            }
            var sortedShops = shops
                .OrderByDescending(s => s.IsActive)
                .ToList();
            return _mapper.ShopToShopResponseDto(sortedShops).ToList();
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
    }
}
