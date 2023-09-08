using Cinema.Application.Showtime.Repositories;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.ValueObjects;
using Cinema.Infrastructure.Common;
using Cinema.Infrastructure.DTOs;
using Microsoft.Extensions.Caching.Distributed;
using System.Text.Json;

namespace Cinema.Infrastructure.Respositories;

public class CachedMoviesRepository : IMoviesRepository
{
    private readonly IMoviesRepository _moviesRepository;
    private readonly IDistributedCache _distributedCache;

    public CachedMoviesRepository(IMoviesRepository moviesRepository, IDistributedCache distributedCache)
    {
        _moviesRepository = moviesRepository;
        _distributedCache = distributedCache;
    }

    public async Task<Movie> Add(Movie movie, CancellationToken cancellationToken)
    {
        return await _moviesRepository.Add(movie, cancellationToken);
    }

    public async Task<Movie?> GetByMovieId(MovieId id, CancellationToken cancellationToken)
    {
        string cacheKey = $"Movie-Id-{id.Value}";

        var cachedMovieEntity = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
        CacheEntity<Movie>? cachedMovie = cachedMovieEntity is null ? null : JsonSerializer.Deserialize<CacheEntity<Movie>>(cachedMovieEntity)!;
        if (cachedMovie is not null && CachedValueIsNotOld(cachedMovie))
            return cachedMovie.Value;

        var movie = await _moviesRepository.GetByMovieId(id, cancellationToken);

        if (movie is null)
            return cachedMovie?.Value;

        await _distributedCache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(new CacheEntity<Movie>
            {
                Value = movie,
                CreatedAt = DateTime.UtcNow
            }),
            cancellationToken);

        return movie;
    }

    public async Task<Movie?> GetByMovieImdbId(string imdbId, CancellationToken cancellationToken)
    {
        string cacheKey = $"Movie_ImdbId_{imdbId}";

        var cachedMovieEntity = await _distributedCache.GetStringAsync(cacheKey, cancellationToken);
        CacheEntity<CachedMovieDTO>? cachedMovie = cachedMovieEntity is null ? null : JsonSerializer.Deserialize<CacheEntity<CachedMovieDTO>>(cachedMovieEntity)!;
        if (cachedMovie is not null && CachedValueIsNotOld(cachedMovie))
            return cachedMovie.Value!.ToDomain();

        var movie = await _moviesRepository.GetByMovieImdbId(imdbId, cancellationToken);

        if (movie is null)
            return cachedMovie?.Value?.ToDomain();

        await _distributedCache.SetStringAsync(
            cacheKey,
            JsonSerializer.Serialize(new CacheEntity<CachedMovieDTO>
            {
                Value = CachedMovieDTO.FromDomain(movie),
                CreatedAt = DateTime.UtcNow
            }),
            cancellationToken);

        return movie;
    }

    public async Task<Movie> Update(Movie movie, CancellationToken cancellationToken)
    {
        return await _moviesRepository.Update(movie, cancellationToken);
    }

    private static bool CachedValueIsNotOld<T>(CacheEntity<T> cachedMovie)
        => DateTime.UtcNow < cachedMovie.CreatedAt.AddMinutes(10);
}
