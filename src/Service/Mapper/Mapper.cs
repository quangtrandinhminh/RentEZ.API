using Repository.Models;
using Repository.Models.Identity;
using Riok.Mapperly.Abstractions;
using Service.Models.Product;
using Service.Models.Shop;
using Service.Models.Shopkeeper;
using Service.Models.User;

namespace Service.Mapper;

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
    public partial void ShopToCreateShop(ShopCreateRequest request, Shop entity);
    public partial Shop MapShopToCreateShop(ShopCreateRequest request);
    public partial IList<ShopResponse> ShopToShopResponseDto(IList<Shop> entity);


    // shopkeeper register
    public partial ShopkeeperRequest MapToShopkeeperResponseDto(UserEntity userEntity);
    public partial ShopResponse MapToShopResponseDto(Shop shop);

    // product
    public partial ProductResponse ProductToProductResponseDto(Product entity);
    public partial IList<ProductResponse> ProductsToProductsResponseDto(IList<Product> entity);

    // datetimeoffset to dateonly
    public DateOnly Map(DateTimeOffset dateTimeOffset)
    {
        return DateOnly.FromDateTime(dateTimeOffset.DateTime);
    }

    // datetime to dateonly
    public DateOnly Map(DateTime dateTime)
    {
        return DateOnly.FromDateTime(dateTime);
    }
}