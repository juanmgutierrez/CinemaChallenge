using Cinema.Domain.Showtime.Entities;

namespace Cinema.Application.Showtime.Repositories;

public interface IMoviesRepository
{
    Task<Movie> Add(
        Movie movie,
        CancellationToken cancellationToken);
}
