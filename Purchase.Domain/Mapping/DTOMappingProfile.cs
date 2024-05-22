
using AutoMapper;
using Purchase.Domain.DTOs;
using Purchase.Domain.DTOs.UserDTOs;
using Purchase.Domain.Models;
using Purchase.Domain.Models.UserEntities;
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

            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
            CreateMap<User, UserDTO>()
              .ReverseMap()
              .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));

            CreateMap<RegisterUserDTO, User>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.Email));
        }
    }

}
