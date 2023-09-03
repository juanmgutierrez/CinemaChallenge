using CinemaAPI;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

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

app.Run();
