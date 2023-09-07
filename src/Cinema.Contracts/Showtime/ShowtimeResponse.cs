namespace Cinema.Contracts.Showtime;

public sealed record ShowtimeResponse(int Id, DateTimeOffset SessionDate, int MovieId, int AuditoriumId)
{
    public static ShowtimeResponse CreateFromDomain(Domain.Showtime.Showtime showtime) =>
        new(
            Id: showtime.Id.Value,
            SessionDate: showtime.SessionDate,
            MovieId: showtime.Movie.Id.Value,
            AuditoriumId: showtime.Auditorium.Id.Value);
}
