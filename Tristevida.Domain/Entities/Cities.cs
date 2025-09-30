using System;

namespace Tristevida.Domain.Entities;

public class Cities
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public int RegionId { get; set; }
    public virtual Regions? Region { get; set; }

    public ICollection<Companies> Companies { get; set; } = new List<Companies>();
    public ICollection<Branches> Branches { get; set; } = new List<Branches>();

    public Cities() { }
    public Cities(string name, int regionId)
    {
        Name = name;
        RegionId = regionId;
    }
}
