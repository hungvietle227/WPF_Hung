using AutoMapper;
using BusinessObject.Models;
using DataAccess.DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;

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
