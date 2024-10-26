using Repository.Extensions;
using Repository.Models;
using Repository.Models.Identity;
using Riok.Mapperly.Abstractions;
using Service.ApiModels.Order;
using Service.BusinessModels;
using Service.ApiModels.Shop;
using Service.ApiModels.Category;
using Service.ApiModels.Shopkeeper;
using Service.ApiModels.Product;
using Service.ApiModels.User;
using Service.ApiModels.OrderDetail;
using Service.ApiModels.OrderShop;
using Service.ApiModels.Voucher;
using Service.ApiModels.PaginatedList;

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
    public partial void ProductToCreateProduct(ProductCreateRequest request, Product entity);
    public partial IList<ProductResponse> ProductsToProductsResponseDto(IList<Product> entity);
    public partial ProductResponse ProductToProductResponseDto(Product entity);

    // category
    public partial void CategoryToCreateCategory(CategoryCreateRequest request, Category entity);
    public partial IList<CategoryResponse> CategoriesToCategoriesResponseDto(IList<Category> entity);
    public partial CategoryResponse CategoryToCategoryResponseDto(Category entity);

    // order
    public partial OrderResponse MapToOrderResponse(Order order);
    public partial IList<OrderListResponse> MapToOrderListResponse(IList<Order> entity);
    public partial Order MapToOrder(OrderRequest request);

    // ordershop
    public partial OrderShop MapToOrderShop(OrderShopRequest request);
    public partial IList<OrderShopResponse> MapToOrderShopListResponse(List<OrderShop> entity);
    public partial PaginatedListResponse<OrderShopListResponse> MapToPaginatedListOrderShopListResponse(PaginatedList<OrderShop> entity);
    public partial OrderShopResponse MapToOrderShopResponse(OrderShop entity);


    // order detail
    public partial IList<OrderDetailListResponse> MapToOrderDetailListResponse(IList<OrderShop> entity);
    public partial OrderDetailResponse MapToOrderDetailResponse(OrderShop entity);
    public partial void Map(OrderDetailRequest request, OrderShop entity);
    public partial OrderDetail MapToOrderShop(OrderDetailRequest request);

    // cart
    public partial List<CartItem> MapToCartItems(List<Product> entity);
    [MapProperty(nameof(Product.Id), nameof(CartItem.ProductId))]
    public partial CartItem MapToCartItem(Product entity);
    public partial void MapRequestToCartItem(CartItemCalcRequest request, CartItem cartItem);
    public partial OrderRequest MapToOrderRequest(Cart cart);
    public partial IList<OrderShopRequest> MapToOrderShopRequest(IList<CartShop> cartShops);
    public partial IList<OrderDetailRequest> MapToOrderDetailRequest(IList<CartItem> cartItems);

    // voucher
    public partial Voucher MapToVoucher(VoucherRequest request);
    public partial VoucherResponse MapToVoucherResponse(Voucher voucher);
    public partial IReadOnlyCollection<VoucherResponse> MapToVoucherResponse(IReadOnlyCollection<Voucher> vouchers);
    public partial PaginatedListResponse<VoucherResponse> MapToPaginatedListVoucherResponse(PaginatedList<Voucher> vouchers);

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