
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

            CreateMap<OrderItem, OrderItemDTO>().ReverseMap();

            CreateMap<Order, OrderDTO>().ReverseMap();
        }
    }

}
