using Cinema.Application.Showtime.Commands.CreateShowtime;
using Cinema.Domain.Auditorium.ValueObjects;

namespace Cinema.Contracts.Showtime;

public sealed record CreateShowtimeRequest(int AuditoriumId, string MovieImdbId, DateTimeOffset SessionDate)
{
    public CreateShowtimeCommand ToCommand() => new(new AuditoriumId(AuditoriumId), MovieImdbId, SessionDate);
}
