using AutoMapper;
using WebApi.DTO.Country;
using WebApi.DTO.States;
using WebApi.Models;

namespace WebApi.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            //Source,Destination
            CreateMap<Country, CreateCountryDto>().ReverseMap();
            CreateMap<Country, CountryDto>().ReverseMap();
            CreateMap<Country, UpdateCountryDto>().ReverseMap();

            CreateMap<States, CreateStatesDto>().ReverseMap();
            CreateMap<States, StatesDto>().ReverseMap();
            CreateMap<States, UpdateStatesDto>().ReverseMap();
        }
    }
}
