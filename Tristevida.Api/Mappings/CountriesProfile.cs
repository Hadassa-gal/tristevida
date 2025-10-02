using System;
using AutoMapper;
using Tristevida.Api.DTOs.Countries;
using Tristevida.Domain.Entities;

namespace Tristevida.Api.Mappings;

public class CountriesProfile : Profile
{
    public CountriesProfile()
    {
        CreateMap<CreateCountryDto, Countries>().ConstructUsing(src => new Countries(src.Name));
        CreateMap<UpdateCountryDto, Countries>().ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name));
        CreateMap<Countries, CountriesDto>();
    }
}
