using Repository.Models;
using Repository.Models.Identity;
using Microsoft.EntityFrameworkCore.Query;
using Riok.Mapperly.Abstractions;
using Service.Models.Product;
using Service.Models.Shop;
using Service.Models.Shopkeeper;
using Service.Models.User;
using Service.Models.Category;

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
    public partial IList<RoleResponse> Map(IList<RoleEntity> entity);
    public partial UserEntity Map(RegisterRequest request);
    public partial LoginResponse Map(UserEntity entity);

    public partial IList<UserResponse> Map(IList<UserEntity> entity);
    public partial IQueryable<UserResponse> Map(IQueryable<UserEntity> entity);
    public partial void Map(RegisterRequest request, UserEntity entity);

    // shop
    public partial void ShopToCreateShop(ShopCreateRequest request, Shop entity);
    public partial Shop MapShopToCreateShop(ShopCreateRequest request);
    public partial IList<ShopResponse> ShopToShopResponseDto(IList<Shop> entity);

    // product
    public partial void ProductToCreateProduct(ProductCreateRequestDto request, Product entity);
    public partial IList<ProductResponse> ProductsToProductsResponseDto(IList<Product> entity);
    public partial ProductResponse ProductToProductResponseDto(Product entity);

    // category
    public partial void CategoryToCreateCategory(CategoryCreateRequest request, Category entity);
    public partial IList<CategoryResponse> CategoriesToCategoriesResponseDto(IList<Category> entity);
    public partial CategoryResponse CategoryToCategoryResponseDto(Category entity);

    // shopkeeper register
    public partial ShopkeeperRequest MapToShopkeeperResponseDto(UserEntity userEntity);
    public partial ShopResponse MapToShopResponseDto(Shop shop);

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

    // role
    public IList<string?> RoleToRoleName(IEnumerable<UserRoleEntity> entity)
    {
        return entity.Select(x => x.Role.NormalizedName).ToList();
    }
}