
using AutoMapper;
using Purchase.Domain.DTOs;
using Purchase.Domain.Models;
namespace Purchase.Domain.Mapping
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Customer, CustomerDTO>().ReverseMap();

            CreateMap<Category, CategoryDTO>().ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();

            CreateMap<Tax, TaxDTO>().ReverseMap();

            CreateMap<OrderItem, OrderItemDTO>()
            .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.Product.Name))
            .ForMember(dest => dest.TaxRate, opt => opt.MapFrom(src => src.Tax != null ? src.Tax.Rate : 0));

            CreateMap<OrderDTO, Order>()
            .ForMember(dest => dest.Customer, opt => opt.Ignore())
            .ForMember(dest => dest.OrderItems, opt => opt.Ignore());
        }
    }

}
