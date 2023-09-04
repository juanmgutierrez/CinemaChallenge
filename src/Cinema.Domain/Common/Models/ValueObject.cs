namespace Cinema.Domain.Common.Models;

public abstract class ValueObject : IEquatable<ValueObject>
{
    public abstract IEnumerable<object> GetEqualityComponents();

    public static bool operator ==(ValueObject? left, ValueObject? right)
    {
        if (left is null ^ right is null)
        {
            return false;
        }

        return left?.Equals(right) != false;
    }

    public static bool operator !=(ValueObject? left, ValueObject? right)
    {
        return !Equals(left, right);
    }

    public override int GetHashCode() =>
        GetEqualityComponents()
        .Select(x => x?.GetHashCode() ?? 0)
        .Aggregate((x, y) => x ^ y);

    public override bool Equals(object? otherObject)
    {
        if (otherObject is null || otherObject.GetType() != GetType())
            return false;

        var valueObject = (ValueObject)otherObject;

        return Equals(valueObject);
    }

    public bool Equals(ValueObject? other) => other is not null && GetEqualityComponents().SequenceEqual(other.GetEqualityComponents());
}
