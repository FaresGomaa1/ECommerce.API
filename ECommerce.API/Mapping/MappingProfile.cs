using AutoMapper;
using ECommerce.API.DTOs.Cart;
using ECommerce.API.DTOs.Order;
using ECommerce.API.DTOs.OrderDetails;
using ECommerce.API.DTOs.Product;
using ECommerce.API.DTOs.ProductSize;
using ECommerce.API.DTOs.WishList;
using ECommerce.API.ECommerce.Domain.Model;
using System.Linq;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, GetProductDTO>()
           .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Select(photo => photo.Url)));

        CreateMap<ProductSizeColor, ProductSizeColorDTO>();
        CreateMap<CartAddEditDTO, Cart>();
        CreateMap<WishListAddEditDTO, Wishlist>();
        CreateMap<OrderDTO, Order>();
        CreateMap<Order, OrderDTO>();
        CreateMap<OrderDetailsDTO, OrderDetails>();
        CreateMap<OrderDetails, OrderDetailsDTO>();
    }
}

