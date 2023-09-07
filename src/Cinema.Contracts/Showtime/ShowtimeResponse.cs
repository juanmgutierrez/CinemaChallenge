namespace Cinema.Contracts.Showtime;

public sealed record ShowtimeResponse(Guid Id, DateTimeOffset SessionDate, Guid MovieId, Guid AuditoriumId)
{
    public static ShowtimeResponse CreateFromDomain(Domain.Showtime.Showtime showtime) =>
        new(
            Id: showtime.Id.Value,
            SessionDate: showtime.SessionDate,
            MovieId: showtime.Movie.Id.Value,
            AuditoriumId: showtime.Auditorium.Id.Value);
}
