using Cinema.Domain.Showtime;

namespace Cinema.UnitTests.Fakes;

internal static class ShowtimeFakes
{
    internal static Showtime ValidShowtime =>
        Showtime.Create(
            DateTimeOffset.UtcNow.AddDays(1),
            MovieFakes.ValidMovie,
            AuditoriumFakes.ValidAuditorium);
}
