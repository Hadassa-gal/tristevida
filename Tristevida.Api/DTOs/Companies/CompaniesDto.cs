namespace Tristevida.Api.DTOs.Companies;

public record class CompaniesDto(Guid Id,string Name,string Ukniu,string Address, int CityId, string Email);
