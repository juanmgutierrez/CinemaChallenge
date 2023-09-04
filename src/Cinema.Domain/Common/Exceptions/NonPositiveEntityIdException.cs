namespace Cinema.Domain.Common.Exceptions;

public class NonPositiveIntEntityIdException : Exception
{
    public NonPositiveIntEntityIdException(string message) : base(message)
    {
    }

    protected static void ThrowIfNonPositive(int id)
    {
        if (id <= 0)
            Throw("Id cannot be less than or equal to zero");
    }

    private static void Throw(string message) => throw new NonPositiveIntEntityIdException(message);
}
