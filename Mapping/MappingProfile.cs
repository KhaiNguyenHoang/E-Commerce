using AutoMapper;
using E_Commerce.DTOs.Auth;
using E_Commerce.DTOs.Cart;
using E_Commerce.DTOs.Category;
using E_Commerce.DTOs.Order;
using E_Commerce.DTOs.Product;
using E_Commerce.DTOs.Review;
using E_Commerce.DTOs.User;
using E_Commerce.Models;

namespace E_Commerce.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // User mappings
        CreateMap<User, UserDto>()
            .ForMember(d => d.RoleName, opt => opt.MapFrom(s => s.Role != null ? s.Role.Name : ""));
        CreateMap<User, UserProfileDto>();
        CreateMap<User, AuthResponseDto>()
            .ForMember(d => d.UserId, opt => opt.MapFrom(s => s.Id))
            .ForMember(d => d.Role, opt => opt.MapFrom(s => s.Role != null ? s.Role.Name : ""));
        CreateMap<RegisterDto, User>()
            .ForMember(d => d.Password, opt => opt.Ignore());
        CreateMap<UserUpdateDto, User>();
        
        // Address mappings
        CreateMap<Address, AddressDto>();
        CreateMap<AddressCreateDto, Address>();
        
        // Category mappings
        CreateMap<Category, CategoryDto>()
            .ForMember(d => d.ProductCount, opt => opt.MapFrom(s => s.Products != null ? s.Products.Count : 0));
        CreateMap<CategoryCreateDto, Category>();
        CreateMap<CategoryUpdateDto, Category>();
        
        // Product mappings
        CreateMap<Product, ProductDto>()
            .ForMember(d => d.CategoryName, opt => opt.MapFrom(s => s.Category != null ? s.Category.Name : ""))
            .ForMember(d => d.Variants, opt => opt.MapFrom(s => s.ProductVariants))
            .ForMember(d => d.Images, opt => opt.MapFrom(s => s.ProductImages));
        CreateMap<ProductCreateDto, Product>();
        CreateMap<ProductUpdateDto, Product>();
        
        // ProductVariant mappings
        CreateMap<ProductVariant, ProductVariantDto>();
        CreateMap<ProductVariantCreateDto, ProductVariant>();
        
        // ProductImage mappings
        CreateMap<ProductImage, ProductImageDto>();
        CreateMap<ProductImageCreateDto, ProductImage>();
        
        // Cart mappings
        CreateMap<Cart, CartDto>()
            .ForMember(d => d.Items, opt => opt.MapFrom(s => s.CartItems));
        CreateMap<CartItem, CartItemDto>()
            .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product != null ? s.Product.Name : ""))
            .ForMember(d => d.ProductImageUrl, opt => opt.MapFrom(s => s.Product != null ? s.Product.MainImageUrl : null))
            .ForMember(d => d.Size, opt => opt.MapFrom(s => s.ProductVariant != null ? s.ProductVariant.Size : null))
            .ForMember(d => d.Color, opt => opt.MapFrom(s => s.ProductVariant != null ? s.ProductVariant.Color : null))
            .ForMember(d => d.VariantId, opt => opt.MapFrom(s => s.ProductVariantId));
        
        // Order mappings
        CreateMap<Order, OrderDto>()
            .ForMember(d => d.Items, opt => opt.MapFrom(s => s.OrderItems));
        CreateMap<OrderItem, OrderItemDto>()
            .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product != null ? s.Product.Name : ""))
            .ForMember(d => d.ProductImageUrl, opt => opt.MapFrom(s => s.Product != null ? s.Product.MainImageUrl : null))
            .ForMember(d => d.Size, opt => opt.MapFrom(s => s.ProductVariant != null ? s.ProductVariant.Size : null))
            .ForMember(d => d.Color, opt => opt.MapFrom(s => s.ProductVariant != null ? s.ProductVariant.Color : null))
            .ForMember(d => d.TotalPrice, opt => opt.MapFrom(s => s.Quantity * s.UnitPrice));
        CreateMap<OrderCreateDto, Order>();
        
        // Review mappings
        CreateMap<Review, ReviewDto>()
            .ForMember(d => d.ProductName, opt => opt.MapFrom(s => s.Product != null ? s.Product.Name : ""))
            .ForMember(d => d.UserName, opt => opt.MapFrom(s => s.User != null ? s.User.FullName : ""))
            .ForMember(d => d.UserAvatarUrl, opt => opt.MapFrom(s => s.User != null ? s.User.AvatarUrl : null));
        CreateMap<ReviewCreateDto, Review>();
        CreateMap<ReviewUpdateDto, Review>();
    }
}
