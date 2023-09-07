namespace Cinema.Contracts.Movie;

public sealed record MovieResponse(
    int Id,
    string Title,
    string FullTitle,
    float? ImdbRating,
    int? ImdbRatingCount,
    short? ReleaseYear,
    string? Image,
    string? Crew,
    float? Stars)
{
    public static MovieResponse CreateFromDomain(Domain.Showtime.Entities.Movie movie) =>
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
