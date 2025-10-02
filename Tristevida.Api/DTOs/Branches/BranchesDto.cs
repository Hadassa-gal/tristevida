namespace Tristevida.Api.DTOs.Branches;

public record class BranchesDto(int Id,int Number_Comercial,string Address, string Email, string Contact_Name, int CityId, int CompanyId);
