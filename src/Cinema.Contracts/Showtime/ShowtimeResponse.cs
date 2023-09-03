using Cinema.Contracts.Auditorium;
using Cinema.Contracts.Movie;

namespace Cinema.Contracts.Showtime;

public record ShowtimeResponse(int Id, DateTimeOffset SessionDate, MovieResponse Movie, AuditoriumResponse Auditorium);
