using AutoMapper;
using Tristevida.Api.DTOs.Regions;
using Tristevida.Domain.Entities;

namespace Tristevida.Api.Mappings;

public class RegionsProfile : Profile
{
    public RegionsProfile()
    {
        CreateMap<CreateRegionDto, Regions>().ConstructUsing(src => new Regions(src.Name, src.CountryId));
        CreateMap<CreateRegionDto, Regions>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.CountryId, opt => opt.MapFrom(src => src.CountryId));
        CreateMap<Regions, RegionsDto>();
    }
}
