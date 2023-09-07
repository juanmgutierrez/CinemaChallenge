using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.ValueObjects;
using MediatR;

namespace Cinema.Application.Showtime.Commands.ReserveTicket;

public sealed record ReserveTicketCommand(ShowtimeId ShowtimeId, short Row, short FromSeatNumber, short ToSeatNumber)
    : IRequest<Ticket>;
