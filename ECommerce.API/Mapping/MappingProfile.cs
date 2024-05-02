using AutoMapper;
using ECommerce.API.DTOs.Product;
using ECommerce.API.DTOs.ProductSize;
using ECommerce.API.ECommerce.Domain.Model;
using System.Linq;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Product, GetProductDTO>()
           .ForMember(dest => dest.Photos, opt => opt.MapFrom(src => src.Photos.Select(photo => photo.Url)));

        CreateMap<ProductSize, ProductSizeDTO>();
    }
}

