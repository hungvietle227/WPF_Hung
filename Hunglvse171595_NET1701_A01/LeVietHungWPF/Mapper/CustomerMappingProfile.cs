using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;

namespace WPF.Utilities.Mappers
{
    public class CustomerMappingProfile : Profile
    {
        public CustomerMappingProfile() 
        {
            CreateMap<Customer, CustomCustomer>().ReverseMap();
        }
    }
}
