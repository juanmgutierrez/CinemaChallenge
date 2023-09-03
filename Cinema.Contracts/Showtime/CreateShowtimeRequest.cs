namespace Cinema.Contracts.Showtime;

internal record CreateShowtimeRequest(int AuditoriumId, int MovieId, DateTimeOffset SessionDate);
