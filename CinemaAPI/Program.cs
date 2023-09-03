using CinemaAPI;
using CinemaAPI.Database;
using CinemaAPI.Database.Repositories;
using CinemaAPI.Database.Repositories.Abstractions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

var builder = WebApplication.CreateBuilder(args);

// TODO ¿Do I need to add Auth?
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

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

// TODO ¿Do I need to add Auth?
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapGet("/movies", async () =>
{
    var c = new ApiClientGrpc();

    return await c.GetAll();
})
.WithName("GetAllMovies")
.WithOpenApi();

app.MapGet("/movie/{id}", async ([FromRoute] string id) =>
{
    var c = new ApiClientGrpc();

    return await c.GetById(id);
})
.WithName("GetMovieById")
.WithOpenApi();

SampleData.Initialize(app);

app.Run();
