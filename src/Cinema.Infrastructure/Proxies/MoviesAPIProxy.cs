using Cinema.Application.Common.Proxies;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.ValueObjects;
using MoviesAPI;

namespace Cinema.Infrastructure.Proxies;

public class MoviesAPIProxy : IMoviesAPIProxy
{
    private readonly MoviesApi.MoviesApiClient _moviesApiClient;

    public MoviesAPIProxy(MoviesApi.MoviesApiClient moviesApiClient)
    {
        _moviesApiClient = moviesApiClient;
    }

    public async Task<Movie?> GetMovie(string imdbId, CancellationToken cancellationToken)
    {
        var request = new GetMovieByIdRequest { Id = imdbId };

        // TODO Secure the API Key
        var headers = new Grpc.Core.Metadata { { "X-Apikey", "68e5fbda-9ec9-4858-97b2-4a8349764c63" } };
        var deadline = DateTime.UtcNow.AddSeconds(5);

        var getByIdReply = await _moviesApiClient.GetByIdAsync(
            request,
            headers,
            deadline,
            cancellationToken);

        getByIdReply.Data.TryUnpack<showResponse>(out var show);

        return show is null ? null : Movie.Create(
            new MovieId(Guid.NewGuid()),
            show.Title,
            show.FullTitle,
            show.Id,
            float.TryParse(show.ImdbRating, out float imdbRating) ? imdbRating : null,
            int.TryParse(show.ImdbRatingCount, out int imdbRatingCount) ? imdbRatingCount : null,
            short.TryParse(show.Year, out short releaseYear) ? releaseYear : null,
            show.Image,
            show.Crew,
            float.TryParse(show.Rank, out float rank) ? rank : null,
            null);
    }
}
