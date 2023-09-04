using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.ValueObjects;

namespace Cinema.Domain.Showtime.Entities;

public sealed class Movie : AggregateRoot<MovieId>
{
    private Movie(MovieId id) : base(id)
    {
    }

    public string Title { get; private set; } = default!;
    public string FullTitle { get; private set; } = default!;
    public string? ImdbRating { get; private set; }
    public string? ImdbRatingCount { get; private set; }
    public short? ReleaseYear { get; private set; }
    public string? Image { get; private set; }
    public string? Crew { get; private set; }
    public string? Stars { get; private set; }

    public static Movie Create(
        MovieId id,
        string title,
        string fullTitle,
        string? imdbRating = default,
        string? imdbRatingCount = default,
        short? releaseYear = default,
        string? image = default,
        string? crew = default,
        string? stars = default)
    {
        return new(id)
        {
            Title = title,
            FullTitle = fullTitle,
            ImdbRating = imdbRating,
            ImdbRatingCount = imdbRatingCount,
            ReleaseYear = releaseYear,
            Image = image,
            Crew = crew,
            Stars = stars
        };
    }
}
