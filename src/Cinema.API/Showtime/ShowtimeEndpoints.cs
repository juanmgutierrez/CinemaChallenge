using Cinema.Application.Showtime.Commands.PayTicket;
using Cinema.Contracts.Showtime;
using Cinema.Domain.Showtime.ValueObjects;
using MediatR;

namespace Cinema.API.Showtime;

public static class ShowtimeEndpoints
{
    public static WebApplication AddShowtimeEndpoints(this WebApplication webApplication)
    {
        var showtimeEndpoints = webApplication.MapGroup("showtime");

        showtimeEndpoints.MapPost("/", async (ISender sender, CreateShowtimeRequest request) =>
        {
            var dbShowtime = await sender.Send(request.ToCommand());
            return ShowtimeResponse.CreateFromDomain(dbShowtime);
        }).Produces<ShowtimeResponse>()
        .WithName("CreateShowtime")
        .WithOpenApi();

        return webApplication;
    }

    public static WebApplication AddTicketEndpoints(this WebApplication webApplication)
    {
        var ticketEndpoints = webApplication.MapGroup("ticket");

        ticketEndpoints.MapPost("reserve", async (ISender sender, ReserveTicketRequest request) =>
        {
            var ticket = await sender.Send(request.ToCommand());
            return ReserveTicketResponse.CreateFromDomain(ticket);
        }).Produces<ReserveTicketResponse>()
        .WithName("ReserveTicket")
        .WithOpenApi();

        ticketEndpoints.MapPost(
            "{id}/pay",
            async (ISender sender, Guid id) => await sender.Send(new PayTicketCommand(new TicketId(id))));

        return webApplication;
    }
}
