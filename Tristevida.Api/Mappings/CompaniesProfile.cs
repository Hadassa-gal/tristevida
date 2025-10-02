using AutoMapper;
using Tristevida.Api.DTOs.Companies;
using Tristevida.Domain.Entities;
using Tristevida.Domain.ValueObjects;

namespace Tristevida.Api.Mappings;

public class CompaniesProfile : Profile
{
    public CompaniesProfile()
    {
        CreateMap<string, Ukniu>()
            .ConvertUsing(src => Ukniu.Create(src));
        CreateMap<Ukniu, string>()
            .ConvertUsing(src => src.Value);
        CreateMap<CreateCompanyDto, Companies>()
            .ConstructUsing(src => new Companies(
                src.Name,
                Ukniu.Create(src.Ukniu),
                src.Address,
                src.Email,
                src.CityId
            ));
        CreateMap<UpdateCompanyDto, Companies>()
            .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
            .ForMember(dest => dest.Ukniu, opt => opt.MapFrom(src => Ukniu.Create(src.Ukniu)))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId));
        CreateMap<Companies, CompaniesDto>()
            .ForMember(dest => dest.Ukniu, opt => opt.MapFrom(src => src.Ukniu.Value));
    }
}
