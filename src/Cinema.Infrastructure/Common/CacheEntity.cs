namespace Cinema.Infrastructure.Common;

public class CacheEntity<T>
{
    public T? Value { get; set; }
    public DateTime CreatedAt { get; set; }
}
