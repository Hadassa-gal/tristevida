using System;

namespace Tristevida.Domain.Entities;

public class Countries
{
    public int Id { get; set; }
    public string Name { get; set; } = null!;

    public ICollection<Regions> Regions { get; set; } = new List<Regions>();

    public Countries() { }
    public Countries(string name)
    {
        Name = name;
    }
}
