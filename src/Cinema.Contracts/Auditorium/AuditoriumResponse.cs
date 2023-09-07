namespace Cinema.Contracts.Auditorium;

public sealed record AuditoriumResponse(Guid Id, int Rows, int SeatsPerRow)
{
    public static AuditoriumResponse CreateFromDomain(Domain.Auditorium.Auditorium auditorium) =>
        new(auditorium.Id.Value, auditorium.Rows, auditorium.SeatsPerRow);
}
