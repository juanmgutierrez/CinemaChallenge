using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.ValueObjects;

namespace Cinema.Domain.Showtime.Entities;

public sealed class Movie : AggregateRoot<MovieId>
{
    private Movie(MovieId id) : base(id)
    {
    }

    // TODO Document changes in this entity
    public string Title { get; private set; } = default!;
    public string FullTitle { get; private set; } = default!;
    public string? ImdbId { get; private set; } = default!;
    public float? ImdbRating { get; private set; }
    public int? ImdbRatingCount { get; private set; }
    public short? ReleaseYear { get; private set; }
    public string? Image { get; private set; }
    public string? Crew { get; private set; }
    public float? Rank { get; private set; }
    public float? Stars { get; private set; }

    public static Movie Create(
        MovieId id,
        string title,
        string fullTitle,
        string? imdbId = default,
        float? imdbRating = default,
        int? imdbRatingCount = default,
        short? releaseYear = default,
        string? image = default,
        string? crew = default,
        float? rank = default,
        float? stars = default)
    {
        return new(id)
        {
            Title = title,
            FullTitle = fullTitle,
            ImdbId = imdbId,
            ImdbRating = imdbRating,
            ImdbRatingCount = imdbRatingCount,
            ReleaseYear = releaseYear,
            Image = image,
            Crew = crew,
            Rank = rank,
            Stars = stars
        };
    }
}
