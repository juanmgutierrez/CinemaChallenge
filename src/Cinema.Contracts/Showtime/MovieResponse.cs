using Cinema.Domain.Showtime.Entities;

namespace Cinema.Contracts.Showtime;

public sealed record MovieResponse(
    Guid Id,
    string Title,
    string FullTitle,
    float? ImdbRating,
    int? ImdbRatingCount,
    short? ReleaseYear,
    string? Image,
    string? Crew,
    float? Stars)
{
    public static MovieResponse CreateFromDomain(Movie movie) =>
        new(
            Id: movie.Id.Value,
            Title: movie.Title,
            FullTitle: movie.FullTitle,
            ImdbRating: movie.ImdbRating,
            ImdbRatingCount: movie.ImdbRatingCount,
            ReleaseYear: movie.ReleaseYear,
            Image: movie.Image,
            Crew: movie.Crew,
            Stars: movie.Stars);
}
