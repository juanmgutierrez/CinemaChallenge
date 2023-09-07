namespace Cinema.Domain.Common.Models;

public abstract class Entity<TId> : IEquatable<Entity<TId>>
    where TId : notnull
{
    public TId Id { get; protected set; }

    protected Entity(TId id)
    {
        Id = id;
    }

    public override bool Equals(object? otherObject) =>
        otherObject is Entity<TId> otherEntity && Equals(otherEntity);

    public override int GetHashCode() => Id.GetHashCode();

    public bool Equals(Entity<TId>? otherEntity) => otherEntity is not null && Id.Equals(otherEntity.Id);

    public static bool operator ==(Entity<TId> left, Entity<TId> right) => Equals(left, right);

    public static bool operator !=(Entity<TId> left, Entity<TId> right) => !Equals(left, right);
}   
