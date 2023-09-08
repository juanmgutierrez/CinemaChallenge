using Cinema.Domain.Showtime;

namespace Cinema.UnitTests.Fakes;

internal static class ShowtimeFakes
{
    internal static Showtime ValidShowtime(DateTimeOffset sessionDate) =>
        Showtime.Create(
            sessionDate,
            MovieFakes.ValidMovie,
            AuditoriumFakes.ValidAuditorium);
}
