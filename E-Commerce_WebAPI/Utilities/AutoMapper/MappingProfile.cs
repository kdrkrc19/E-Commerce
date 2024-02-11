using AutoMapper;
using Entities.Models;
using Entities.ModelsDTO;

namespace E_Commerce_WebAPI.Utilities.AutoMapper
{
    public class MappingProfile : Profile 
    {
        public MappingProfile()
        {
            CreateMap<ModelsDto, Model>();
            CreateMap<BrandsDto, Brand>();
            CreateMap<ProductsDto, Product>();
            CreateMap<UserForRegistrationDto, User>();
            CreateMap<EmailUsDto, EmailUs>();
        }
    }
}
