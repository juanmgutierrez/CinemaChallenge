using Cinema.Application.Common.Proxies;
using Cinema.Application.Showtime.Repositories;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.ValueObjects;
using Cinema.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Respositories;

internal class MoviesRepository : IMoviesRepository
{
    private readonly CinemaDbContext _context;
    private readonly IMoviesAPIProxy _moviesAPIProxy;

    public MoviesRepository(CinemaDbContext context, IMoviesAPIProxy moviesAPIProxy)
    {
        _context = context;
        _moviesAPIProxy = moviesAPIProxy;
    }

    public async Task<Movie?> GetByMovieId(MovieId id, CancellationToken cancellationToken)
    {
        var movie = await _context.Movies
            .Where(m => m.Id == id)
            .FirstOrDefaultAsync(cancellationToken);

        if (movie?.ImdbId is null)
            return null;

        var movieFromAPIProxy = await _moviesAPIProxy.GetMovie(movie.ImdbId, cancellationToken);

        if (movieFromAPIProxy is not null)
            movie.UpdateMetadata(movieFromAPIProxy);

        var updatedMovie = await Update(movie, cancellationToken);
        return updatedMovie;
    }

    public async Task<Movie?> GetByMovieImdbId(string imdbId, CancellationToken cancellationToken)
    {
        var movieFromAPIProxy = await _moviesAPIProxy.GetMovie(imdbId, cancellationToken);

        if (movieFromAPIProxy is null)
            return null;

        var movie = await _context.Movies
            .Where(m => m.ImdbId == imdbId)
            .FirstOrDefaultAsync(cancellationToken);

        if (movie is not null)
        {
            movie.UpdateMetadata(movieFromAPIProxy);
            var updatedMovie = await Update(movie, cancellationToken);
            return updatedMovie;
        }
        else
        {
            var addedMovie = await Add(movieFromAPIProxy, cancellationToken);
            return addedMovie;
        }
    }

    public async Task<Movie> Add(Movie movie, CancellationToken cancellationToken)
    {
        var addedMovie = _context.Add(movie);
        await _context.SaveChangesAsync(cancellationToken);
        return addedMovie.Entity;
    }

    public async Task<Movie> Update(Movie movie, CancellationToken cancellationToken)
    {
        var updatedMovie = _context.Update(movie);
        await _context.SaveChangesAsync(cancellationToken);
        return updatedMovie.Entity;
    }
}
