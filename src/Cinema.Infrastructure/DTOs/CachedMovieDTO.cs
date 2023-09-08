using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.ValueObjects;

namespace Cinema.Infrastructure.DTOs;

public record CachedMovieDTO(
    Guid Id,
    string Title,
    string FullTitle,
    string? ImdbId,
    float? ImdbRating,
    int? ImdbRatingCount,
    short? ReleaseYear,
    string? Image,
    string? Crew,
    float? Rank,
    float? Stars)
{
    public static CachedMovieDTO FromDomain(Movie movie) =>
        new(
            movie.Id.Value,
            movie.Title,
            movie.FullTitle,
            movie.ImdbId,
            movie.ImdbRating,
            movie.ImdbRatingCount,
            movie.ReleaseYear,
            movie.Image,
            movie.Crew,
            movie.Rank,
            movie.Stars);

    public Movie ToDomain() =>
        Movie.Create(
            new MovieId(Id),
            Title,
            FullTitle,
            ImdbId,
            ImdbRating,
            ImdbRatingCount,
            ReleaseYear,
            Image,
            Crew,
            Rank,
            Stars);
}
