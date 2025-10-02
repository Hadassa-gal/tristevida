using AutoMapper;
using Tristevida.Api.DTOs.Cities;
using Tristevida.Domain.Entities;

namespace Tristevida.Api.Mappings;

public class CitiesProfile : Profile
{
    public CitiesProfile()
    {
        CreateMap<CreateCityDto, Cities>()
            .ConstructUsing(src => new Cities(src.Name, src.RegionId));
        CreateMap<UpdateCityDto, Cities>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.RegionId, opt => opt.MapFrom(src => src.RegionId));
        CreateMap<Cities, CitiesDto>();
    }
}
