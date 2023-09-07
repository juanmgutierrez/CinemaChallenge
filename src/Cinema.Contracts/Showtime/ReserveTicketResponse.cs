using Cinema.Contracts.Auditorium;
using Cinema.Domain.Showtime.Entities;

namespace Cinema.Contracts.Showtime;

public sealed record ReserveTicketResponse(Guid TicketId, Guid AuditoriumId, MovieResponse Movie, List<SeatResponse> Seats, DateTimeOffset CreatedAt)
{
    public static ReserveTicketResponse CreateFromDomain(Ticket ticket) =>
        new(
            ticket.Id.Value,
            ticket.Showtime.Auditorium.Id.Value,
            MovieResponse.CreateFromDomain(ticket.Showtime.Movie),
            ticket.Seats.Select(SeatResponse.CreateFromDomain).ToList(),
            ticket.CreatedAt);
}

//public sealed record ReserveTicketResponse(Guid TicketId, int AuditoriumId, List<SeatResponse> Seats, DateTimeOffset CreatedAt)
//{
//    public static ReserveTicketResponse CreateFromDomain(Ticket ticket) =>
//        new(
//            ticket.Id.Value,
//            ticket.Showtime.Auditorium.Id.Value,
//            ticket.Seats.Select(SeatResponse.CreateFromDomain).ToList(),
//            ticket.CreatedAt);
//}
