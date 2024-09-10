using BusinessObject.DTO.Product;
using BusinessObject.DTO.Shop;
using BusinessObject.DTO.Shopkeeper;
using BusinessObject.DTO.User;
using BusinessObject.Entities;
using BusinessObject.Entities.Identity;
using BusinessObject.Entities.Product;
using BusinessObject.Entities.Shop;
using Microsoft.EntityFrameworkCore.Query;
using Riok.Mapperly.Abstractions;

namespace BusinessObject.Mapper;

[Mapper]
public partial class MapperlyMapper
{
    //  public partial Entity UserToLoginResponseDto(Dto request); --for create
    // public partial ResponseDTO UserToLoginResponseDto(Entity entity); --for get 
    // public partial IList<ResponseDTO> UserToLoginResponseDto(IList<Entity> entity); --for get all
    // public partial IQueryable<ResponseDTO> UserToLoginResponseDto(IQueryable<Entity> entity); --for get all
    // public partial void UserToLoginResponseDto(Dto request, Entity entity); --for update

    // Custom mapping method for IList<Pet> to IList<PetResponseDto> with date formatting
    // public List<PetResponseDto?> Map(List<Pet> entities)
    // {
    //     return entities.Select(e => Map(e)).ToList();
    // }

    // user
    public partial IList<RoleResponseDto> Map(IList<RoleEntity> entity);
    public partial UserEntity Map(RegisterDto request);
    public partial LoginResponseDto UserToLoginResponseDto(UserEntity entity);
    public partial UserResponseDto UserToUserResponseDto(UserEntity entity);
    public partial IList<UserResponseDto> Map(IList<UserEntity> entity);
    public partial IQueryable<UserResponseDto> Map(IQueryable<UserEntity> entity);
    public partial void Map(RegisterDto request, UserEntity entity);

    // shop
    public partial void ShopToCreateShop(ShopCreateRequestDto request, ShopEntity entity);
    public partial ShopEntity MapShopToCreateShop(ShopCreateRequestDto request);
    public partial IList<ShopResponseDto> ShopToShopResponseDto(IList<ShopEntity> entity);


    // shopkeeper register
    public partial UserResponseDto MapToUserResponseDto(UserEntity userEntity);
    public partial ShopResponseDto MapToShopResponseDto(ShopEntity shopEntity);
    public ShopkeeperRegisterResponseDto MapToShopkeeperRegisterResponseDto(UserEntity userEntity, ShopEntity shopEntity)
    {
        var userResponse = MapToUserResponseDto(userEntity);
        var shopResponse = MapToShopResponseDto(shopEntity);
        return new ShopkeeperRegisterResponseDto
        {
            Id = userResponse.Id,
            UserName = userResponse.UserName,
            Email = userResponse.Email,
            PhoneNumber = userResponse.PhoneNumber,
            FullName = userResponse.FullName,
            Address = userResponse.Address,
            Avatar = userResponse.Avatar,
            BirthDate = userResponse.BirthDate,

            ShopEmail = shopResponse.ShopEmail,
            ShopName = shopResponse.ShopName,
            Shop_Address = shopResponse.Shop_Address,
            Shop_Avatar = shopResponse.Shop_Avatar,
            Shop_Phone = shopResponse.Shop_Phone
        };
    }
    public ShopkeeperRegisterRequestDto MapToShopkeeperRegisterRequestDto(UserEntity userEntity, ShopEntity shopEntity)
    {
        var userResponse = MapToUserResponseDto(userEntity);
        var shopResponse = MapToShopResponseDto(shopEntity);
        return new ShopkeeperRegisterRequestDto
        {
            UserName = userResponse.UserName,
            Email = userResponse.Email,
            PhoneNumber = userResponse.PhoneNumber,
            FullName = userResponse.FullName,
            Address = userResponse.Address,
            Avatar = userResponse.Avatar,
            BirthDate = userResponse.BirthDate,

            ShopEmail = shopResponse.ShopEmail,
            ShopName = shopResponse.ShopName,
            Shop_Address = shopResponse.Shop_Address,
            Shop_Avatar = shopResponse.Shop_Avatar,
            Shop_Phone = shopResponse.Shop_Phone
        };
    }

    // product
    public partial ProductResponseDto ProductToProductResponseDto(ProductEntity entity);
    public partial IList<ProductResponseDto> ProductsToProductsResponseDto(IList<ProductEntity> entity);

    // datetimeoffset to dateonly
    public DateOnly Map(DateTimeOffset dateTimeOffset)
    {
        return DateOnly.FromDateTime(dateTimeOffset.DateTime);
    }
}