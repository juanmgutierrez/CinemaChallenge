namespace Cinema.Application.Showtime.Commands;

// TODO Crear IDs tipados, no int
public record CreateShowtimeCommand(int AuditoriumId, int MovieId, DateTimeOffset SessionDate);
