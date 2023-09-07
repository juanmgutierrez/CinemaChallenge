using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.ValueObjects;

namespace Cinema.Application.Showtime.Repositories;

public interface IMoviesRepository
{
    Task<Movie?> GetByMovieId(MovieId id, CancellationToken cancellationToken);
    Task<Movie?> GetByMovieImdbId(string imdbId, CancellationToken cancellationToken);
    Task<Movie> Add(Movie movie, CancellationToken cancellationToken);
    Task<Movie> Update(Movie movie, CancellationToken cancellationToken);
}
