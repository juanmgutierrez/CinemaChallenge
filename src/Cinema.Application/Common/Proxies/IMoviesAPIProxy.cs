using Cinema.Domain.Showtime.Entities;

namespace Cinema.Application.Common.Proxies;

public interface IMoviesAPIProxy
{
    public Task<Movie> GetMovie(string imdbId, CancellationToken cancellationToken);
}