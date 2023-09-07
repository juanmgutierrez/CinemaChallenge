using Cinema.API.Common;
using Cinema.API.Showtime;
using Cinema.Infrastructure.InitializationExtensions;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddCQRSBehavior();

builder.Services.AddInfrastructure(builder.Environment);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();

app.AddShowtimeEndpoints();
app.AddTicketEndpoints();

if (app.Environment.IsDevelopment())
{
    app.CreateDatabase();
    app.SeedDatabase();
}

app.Run();
