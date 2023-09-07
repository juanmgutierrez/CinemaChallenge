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

var showtimeEndpoints = app.MapGroup("showtime");

showtimeEndpoints.MapPost("/", async (ISender sender, CreateShowtimeRequest request) =>
{
    var dbShowtime = await sender.Send(request.ToCommand());
    return ShowtimeResponse.CreateFromDomain(dbShowtime);
}).Produces<ShowtimeResponse>()
.WithName("CreateShowtime")
.WithOpenApi();

var ticketEndpoints = app.MapGroup("ticket");

ticketEndpoints.MapPost("reserve", async (ISender sender, ReserveTicketRequest request) =>
{
    var ticket = await sender.Send(request.ToCommand());
    return ReserveTicketResponse.CreateFromDomain(ticket);
}).Produces<ReserveTicketResponse>()
.WithName("ReserveTicket")
.WithOpenApi();

//ticketEndpoints.MapPost("buy", async (ISender sender, CreateShowtimeRequest request) =>
//{
//    var dbShowtime = await sender.Send(request.ToCommand());
//    return ShowtimeResponse.CreateFromDomain(dbShowtime);
//});

if (app.Environment.IsDevelopment())
{
    app.CreateDatabase();
    app.SeedDatabase();
}

app.Run();
