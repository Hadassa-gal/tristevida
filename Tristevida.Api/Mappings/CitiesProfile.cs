using AutoMapper;
using Tristevida.Api.DTOs.Cities;
using Tristevida.Domain.Entities;

namespace Tristevida.Api.Mappings;

public class CitiesProfile : Profile
{
    public CitiesProfile()
    {
        // DTO → Entity
        CreateMap<CreateCityDto, Cities>()
            .ConstructUsing(src => new Cities(src.Name, src.RegionId));

        // Entity → DTO
        CreateMap<Cities, CitiesDto>();
    }
}
