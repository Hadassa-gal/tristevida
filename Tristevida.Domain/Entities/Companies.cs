using System;
using Tristevida.Domain.ValueObjects;

namespace Tristevida.Domain.Entities;

public class Companies
{
    public Guid Id { get; set; } = Guid.NewGuid();
    public string Name { get; set; } = null!;
    public Ukniu Ukniu { get; set; } = null!;
    public string Address { get; set; } = null!;
    public string Email { get; set; } = null!;

    public int CityId { get; set; }
    public virtual Cities? City { get; set; }

    public ICollection<Branches> Branches { get; set; } = new List<Branches>();

    public Companies() { }
    public Companies(string name, Ukniu ukniu, string address, string email, int cityId)
    {
        Name = name;
        Ukniu = ukniu;
        Address = address;
        Email = email;
        CityId = cityId;
    }
}
