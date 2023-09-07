using Cinema.Application.Showtime.Repositories;
using Cinema.Domain.Showtime.Entities;
using Cinema.Infrastructure.Contexts;

namespace Cinema.Infrastructure.Respositories;

internal class MoviesRepository : IMoviesRepository
{
    private readonly CinemaDbContext _context;

    public MoviesRepository(CinemaDbContext context)
    {
        _context = context;
    }

    public async Task<Movie> Add(Movie movie, CancellationToken cancellationToken)
    {
        var addedMovie = _context.Add(movie);
        await _context.SaveChangesAsync(cancellationToken);
        return addedMovie.Entity;
    }
}
