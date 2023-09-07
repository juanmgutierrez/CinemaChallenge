using MediatR;

namespace Cinema.Application.Showtime.Commands.PayTicket;

public sealed record PayTicketCommand(Domain.Showtime.ValueObjects.TicketId Id) : IRequest;
