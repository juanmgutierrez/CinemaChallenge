namespace Cinema.Domain.Common.Models;

public abstract class EntityId<TId> : ValueObject
    where TId : notnull
{
    public EntityId(TId value)
    {
        Value = value;
    }

    public TId Value { get; init; }

    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return Value;
    }
}
