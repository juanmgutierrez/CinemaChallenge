using Cinema.Application.Showtime.Commands;

namespace Cinema.Contracts.Showtime;

public record CreateShowtimeRequest(int AuditoriumId, int MovieId, DateTimeOffset SessionDate)
{
    public CreateShowtimeCommand ToCommand() => new(AuditoriumId, MovieId, SessionDate);
}
