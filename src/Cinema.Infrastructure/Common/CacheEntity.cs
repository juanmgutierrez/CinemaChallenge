namespace Cinema.Infrastructure.Common;

internal class CacheEntity<T>
{
    internal T? Value { get; set; }
    internal DateTime CreatedAt { get; set; }
}
