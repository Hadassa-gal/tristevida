using System;

namespace Tristevida.Domain.ValueObjects;

public class Ukniu: IEquatable<Ukniu>
{
    protected Ukniu() { }
    public string Value { get; private set; } = null!;
    private Ukniu(string value) => Value = value.ToUpperInvariant().Trim();
    public static Ukniu Create(string value)
    {
        if (string.IsNullOrWhiteSpace(value)) throw new ArgumentException("Ukniu vacío");
        return new Ukniu(value);
    }
    public bool Equals(Ukniu? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => obj is Ukniu o && Equals(o);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;
}
