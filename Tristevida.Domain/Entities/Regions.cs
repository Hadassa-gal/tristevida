using System;

namespace Tristevida.Domain.Entities;

public class Regions
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int CountryId { get; set; }
    public virtual Countries? Country { get; set; }

    public ICollection<Cities> Cities { get; set; } = new List<Cities>();

    public Regions() { }
    public Regions(string name, int countryId)
    {
        Name = name;
        CountryId = countryId;
    }
}
