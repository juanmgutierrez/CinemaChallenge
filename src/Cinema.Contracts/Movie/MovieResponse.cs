namespace Cinema.Contracts.Movie;

public record MovieResponse(
    int Id,
    string? Title,
    string? FullTitle,
    string? ImdbRating,
    string? ImdbRatingCount,
    short? ReleaseYear,
    string? Image,
    string? Crew,
    string? Stars);
