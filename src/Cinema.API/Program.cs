using Cinema.API.Errors;
using Cinema.Application.Common;
using Cinema.Application.Common.Behaviours;
using Cinema.Contracts.Showtime;
using Cinema.Infrastructure.InitializationExtensions;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMediatR(cfg =>
{
    cfg.RegisterServicesFromAssembly(typeof(ApplicationAssemblyReference).Assembly);

    // TODO Add Logging
    //config.AddBehavior<LoggingBehavior>();

    cfg.AddBehavior(typeof(IPipelineBehavior<,>), typeof(RequestValidationPipelineBehavior<,>));
});

builder.Services.AddValidatorsFromAssembly(typeof(ApplicationAssemblyReference).Assembly);

//builder.Services.AddProblemDetailsFactory(builder.Environment);
if (builder.Environment.IsProduction())
    builder.Services.AddSingleton<ProblemDetailsFactory, CinemaProblemDetailsFactory>();
else
    builder.Services.AddSingleton<ProblemDetailsFactory, CinemaDebugProblemDetailsFactory>();

builder.Services.AddInfrastructure(builder.Environment.IsDevelopment());

builder.Services.Configure<JsonOptions>(options => options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

var externalAPIEndpoints = app.MapGroup("external-api");

#region External API Endpoints

//// TODO Check URL naming convention in all routes
//externalAPIEndpoints.MapGet("movies", async () =>
//{
//    var c = new ApiClientGrpc();

//    return await c.GetAll();
//})
//.WithName("GetAllMovies")
//.WithOpenApi();

//externalAPIEndpoints.MapGet("movie/{id}", async ([FromRoute] string id) =>
//{
//    var c = new ApiClientGrpc();

//    return await c.GetById(id);
//})
//.WithName("GetMovieById")
//.WithOpenApi();

#endregion

var showtimeEndpoints = app.MapGroup("showtime");

#region Showtime Endpoints

showtimeEndpoints.MapPost("/", async (ISender sender, CreateShowtimeRequest request) =>
{
    var dbShowtime = await sender.Send(request.ToCommand());
    return ShowtimeResponse.CreateFromDomain(dbShowtime);
});

// TODO Remove if not anymore needed

#region GET

//showtimeEndpoints.MapGet("{id}", async (IShowtimesRepository showtimesRepository, int id) =>
//{
//    var showtime = await showtimesRepository.Get(id, CancellationToken.None, true, true);

//    if (showtime is null)
//        return Results.NotFound();

//    ShowtimeResponse response = new(
//        showtime.Id,
//        showtime.SessionDate,
//        new MovieResponse(
//            showtime.Movie.Id,
//            showtime.Movie.Title,
//            showtime.Movie.FullTitle,
//            showtime.Movie.ImdbRating,
//            showtime.Movie.ImdbRatingCount,
//            showtime.Movie.ReleaseYear,
//            showtime.Movie.Image,
//            showtime.Movie.Crew,
//            showtime.Movie.Stars),
//        new AuditoriumResponse(showtime.AuditoriumId));

//    return Results.Ok(response);
//})
//.Produces<ShowtimeResponse>()
//.WithName("GetShowtime")
//.WithOpenApi();

#endregion

#endregion

//var ticketEndpoints = app.MapGroup("ticket");

////Reserve seats.
////Buy seats.

if (app.Environment.IsDevelopment())
{
    app.CreateDatabase();
    app.SeedDatabase();
}

app.Run();
