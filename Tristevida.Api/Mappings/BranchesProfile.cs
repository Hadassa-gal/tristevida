using AutoMapper;
using Tristevida.Api.DTOs.Branches;
using Tristevida.Domain.Entities;

namespace Tristevida.Api.Mappings;

public class BranchesProfile : Profile
{
    public BranchesProfile()
    {
        CreateMap<CreateBranchesDto, Branches>()
            .ConstructUsing(src => new Branches(
                src.Number_Comercial,
                src.Address,
                src.Email,
                src.Contact_Name,
                src.CityId,
                src.CompanyId
            ));
        CreateMap<UpdateBranchesDto, Branches>()
            .ForMember(dest => dest.Number_Comercial, opt => opt.MapFrom(src => src.Number_Comercial))
            .ForMember(dest => dest.Address, opt => opt.MapFrom(src => src.Address))
            .ForMember(dest => dest.Email, opt => opt.MapFrom(src => src.Email))
            .ForMember(dest => dest.Contact_Name, opt => opt.MapFrom(src => src.Contact_Name))
            .ForMember(dest => dest.CityId, opt => opt.MapFrom(src => src.CityId))
            .ForMember(dest => dest.CompanyId, opt => opt.MapFrom(src => src.CompanyId));
        CreateMap<Branches, BranchesDto>();
    }
}
