using Cinema.Contracts.Auditorium;
using Cinema.Contracts.Movie;
using Cinema.Contracts.Showtime;
using CinemaAPI;
using CinemaAPI.Database;
using CinemaAPI.Database.Entities;
using CinemaAPI.Database.Repositories;
using CinemaAPI.Database.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// TODO Do I need to add Auth?
builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddTransient<IShowtimesRepository, ShowtimesRepository>();
builder.Services.AddTransient<ITicketsRepository, TicketsRepository>();
builder.Services.AddTransient<IAuditoriumsRepository, AuditoriumsRepository>();

builder.Services.AddDbContext<CinemaContext>(options =>
{
    options.UseInMemoryDatabase("CinemaDb");

    if (builder.Environment.IsDevelopment())
    {
        options.EnableSensitiveDataLogging();
        options.EnableDetailedErrors();
        options.ConfigureWarnings(b => b.Ignore(InMemoryEventId.TransactionIgnoredWarning));
    }

    // TODO ¿Do I specify the memory cache for query caching?
});

builder.Services.Configure<JsonOptions>(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// TODO Do I need to add Auth?
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

var externalAPIEndpoints = app.MapGroup("external-api");

#region External API Endpoints

// TODO Check URL naming convention in all routes
externalAPIEndpoints.MapGet("movies", async () =>
{
    var c = new ApiClientGrpc();

    return await c.GetAll();
})
.WithName("GetAllMovies")
.WithOpenApi();

externalAPIEndpoints.MapGet("movie/{id}", async ([FromRoute] string id) =>
{
    var c = new ApiClientGrpc();

    return await c.GetById(id);
})
.WithName("GetMovieById")
.WithOpenApi();

#endregion

var showtimeEndpoints = app.MapGroup("showtime");

#region Showtime Endpoints

showtimeEndpoints.MapPost("/", async (CreateShowtimeRequest request, IShowtimesRepository showtimesRepository, HttpContext context) =>
{
    var c = new ApiClientGrpc();
    var movie = await c.GetById(request.MovieId);

    MovieEntity movieEntity = new()
    {
        Title = movie.Title,
        FullTitle = movie.Title,
        ReleaseYear = short.TryParse(movie.Year, out short year) ? year : null,
        Crew = movie.Crew,
        Image = movie.Image,
        ImdbRating = movie.ImdbRating,
        ImdbRatingCount = movie.ImdbRatingCount
    };

    var showtime = await showtimesRepository.CreateShowtime(
        new ShowtimeEntity
        {
            Movie = movieEntity,
            SessionDate = request.SessionDate,
            AuditoriumId = request.AuditoriumId
        },
        CancellationToken.None);

    ShowtimeResponse response = new(
        showtime.Id,
        showtime.SessionDate,
        new MovieResponse(
            showtime.Movie.Id,
            showtime.Movie.Title,
            showtime.Movie.FullTitle,
            showtime.Movie.ImdbRating,
            showtime.Movie.ImdbRatingCount,
            showtime.Movie.ReleaseYear,
            showtime.Movie.Image,
            showtime.Movie.Crew,
            showtime.Movie.Stars),
        new AuditoriumResponse(showtime.AuditoriumId));

    return Results.Created(new Uri($"{context.Request.Host}{context.Request.Path}/{showtime.Id}"), response);
})
.Produces<ShowtimeResponse>()
.WithName("CreateShowtime")
.WithOpenApi();

showtimeEndpoints.MapGet("{id}", async (IShowtimesRepository showtimesRepository, int id) =>
{
    var showtime = await showtimesRepository.Get(id, CancellationToken.None, true, true);

    if (showtime is null)
        return Results.NotFound();

    ShowtimeResponse response = new(
        showtime.Id,
        showtime.SessionDate,
        new MovieResponse(
            showtime.Movie.Id,
            showtime.Movie.Title,
            showtime.Movie.FullTitle,
            showtime.Movie.ImdbRating,
            showtime.Movie.ImdbRatingCount,
            showtime.Movie.ReleaseYear,
            showtime.Movie.Image,
            showtime.Movie.Crew,
            showtime.Movie.Stars),
        new AuditoriumResponse(showtime.AuditoriumId));

    return Results.Ok(response);
})
.Produces<ShowtimeResponse>()
.WithName("GetShowtime")
.WithOpenApi();

#endregion

//var ticketEndpoints = app.MapGroup("ticket");

////Reserve seats.
////Buy seats.

SampleData.Initialize(app);

app.Run();

record CreateShowtimeRequest(string MovieId, DateTime SessionDate, int AuditoriumId);
