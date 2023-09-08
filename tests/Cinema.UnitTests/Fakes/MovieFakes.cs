using Cinema.Domain.Showtime.Entities;

namespace Cinema.UnitTests.Fakes;

internal static class MovieFakes
{
    internal static Movie ValidMovie =>
        Movie.Create(
            new(FakeConstants.MovieId),
            "Movie Title",
            "Movie Full Title",
            "tt1234567",
            (float?)5.1,
            50000,
            2023,
            "imageURL",
            "CosmeFulanito&others",
            15,
            null);
}
