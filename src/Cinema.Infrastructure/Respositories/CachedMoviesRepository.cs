using Cinema.Application.Showtime.Repositories;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.ValueObjects;
using Cinema.Infrastructure.Common;
using Microsoft.Extensions.Caching.Memory;

namespace Cinema.Infrastructure.Respositories;

public class CachedMoviesRepository : IMoviesRepository
{
    private readonly IMoviesRepository _moviesRepository;
    private readonly IMemoryCache _memoryCache;

    public CachedMoviesRepository(IMoviesRepository moviesRepository, IMemoryCache memoryCache)
    {
        _moviesRepository = moviesRepository;
        _memoryCache = memoryCache;
    }

    public async Task<Movie> Add(Movie movie, CancellationToken cancellationToken)
    {
        return await _moviesRepository.Add(movie, cancellationToken);
    }

    public async Task<Movie?> GetByMovieId(MovieId id, CancellationToken cancellationToken)
    {
        string cacheKey = $"Movie-Id-{id.Value}";

        if (_memoryCache.TryGetValue(cacheKey, out CacheEntity<Movie>? cachedMovie)
            && CachedValueIsNotOld(cachedMovie!))
            return cachedMovie!.Value;

        var movie = await _moviesRepository.GetByMovieId(id, cancellationToken);

        if (movie is null)
            return cachedMovie?.Value;

        _memoryCache.Set(cacheKey, new CacheEntity<Movie>
        {
            Value = movie,
            CreatedAt = DateTime.UtcNow
        });

        return movie;
    }

    public async Task<Movie?> GetByMovieImdbId(string imdbId, CancellationToken cancellationToken)
    {
        string cacheKey = $"Movie-ImdbId-{imdbId}";

        if (_memoryCache.TryGetValue(cacheKey, out CacheEntity<Movie>? cachedMovie)
            && CachedValueIsNotOld(cachedMovie!))
            return cachedMovie!.Value;

        var movie = await _moviesRepository.GetByMovieImdbId(imdbId, cancellationToken);

        if (movie is null)
            return cachedMovie?.Value;

        _memoryCache.Set(cacheKey, new CacheEntity<Movie>
        {
            Value = movie,
            CreatedAt = DateTime.UtcNow
        });

        return movie;
    }

    public async Task<Movie> Update(Movie movie, CancellationToken cancellationToken)
    {
        return await _moviesRepository.Update(movie, cancellationToken);
    }

    private static bool CachedValueIsNotOld(CacheEntity<Movie> cachedMovie)
        => DateTime.UtcNow < cachedMovie.CreatedAt.AddMinutes(10);
}
