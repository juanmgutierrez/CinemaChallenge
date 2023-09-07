using Cinema.Application.Showtime.Commands.CreateShowtime;
using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Showtime.ValueObjects;

namespace Cinema.Contracts.Showtime;

public sealed record CreateShowtimeRequest(Guid AuditoriumId, DateTimeOffset SessionDate, Guid? MovieId = null, string? MovieImdbId = null)
{
    public CreateShowtimeCommand ToCommand() =>
        new(
            new AuditoriumId(AuditoriumId),
            SessionDate,
            MovieId is null ? null : new MovieId(MovieId.Value),
            MovieImdbId);
}
