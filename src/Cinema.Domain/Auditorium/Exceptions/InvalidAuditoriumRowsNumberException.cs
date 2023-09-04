namespace Cinema.Domain.Auditorium.Exceptions;

public sealed class InvalidAuditoriumRowsNumberException : Exception
{
    public InvalidAuditoriumRowsNumberException(int rowsNumber) : base($"Invalid auditorium rows number: {rowsNumber}")
    {
    }
}
