using Grpc.Net.Client;
using MoviesAPI;

namespace CinemaAPI;

public class ApiClientGrpc
{
    public async Task<showListResponse> GetAll()
    {
        // TODO Inject as Singleton
        using var channel = GrpcChannel.ForAddress(
            // TODO Extract to appsettings.json
            "https://localhost:7443",
            new GrpcChannelOptions()
            {
                // TODO Validate environment (only for Development)
                HttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                }
            });

        var client = new MoviesApi.MoviesApiClient(channel);
        // TODO Secure the API Key
        var headers = new Grpc.Core.Metadata { { "X-Apikey", "68e5fbda-9ec9-4858-97b2-4a8349764c63" } };
        // TODO Add Deadline and Cancellation Token
        var getAllReply = await client.GetAllAsync(new Empty(), headers);
        getAllReply.Data.TryUnpack<showListResponse>(out var movies);

        Console.WriteLine($"Total Movies: {movies.Movies.Count}");
        foreach(var movie in movies.Movies)
        {
            Console.WriteLine($"Movie: {movie.Title}");
        }

        return movies;
    }

    public async Task<showResponse> GetById(string movieId)
    {
        // TODO Inject as Singleton
        using var channel = GrpcChannel.ForAddress(
            // TODO Extract to appsettings.json
            "https://localhost:7443",
            new GrpcChannelOptions()
            {
                // TODO Validate environment (only for Development)
                HttpHandler = new HttpClientHandler
                {
                    ServerCertificateCustomValidationCallback = HttpClientHandler.DangerousAcceptAnyServerCertificateValidator
                }
            });

        var client = new MoviesApi.MoviesApiClient(channel);
        // TODO Secure the API Key
        var headers = new Grpc.Core.Metadata { { "X-Apikey", "68e5fbda-9ec9-4858-97b2-4a8349764c63" } };
        // TODO Add Deadline and Cancellation Token
        var getByIdReply = await client.GetByIdAsync(new GetMovieByIdRequest { Id = movieId }, headers);
        getByIdReply.Data.TryUnpack<showResponse>(out var shows);
        return shows;
    }
}
