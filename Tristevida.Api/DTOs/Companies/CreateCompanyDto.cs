namespace Tristevida.Api.DTOs.Companies;

public record class CreateCompanyDto(string Name,string Ukniu,string Address, int CityId, string Email);
