﻿using Cinema.Domain.Showtime.Exceptions;
using CinemaAPI.Database.Repositories.Abstractions;
using MediatR;

namespace Cinema.Application.Showtime.Commands.PayTicket;

public sealed class PayTicketCommandHandler : IRequestHandler<PayTicketCommand>
{
    private readonly ITicketsRepository _ticketsRepository;

    public PayTicketCommandHandler(ITicketsRepository ticketsRepository) =>
        _ticketsRepository = ticketsRepository;

    public async Task Handle(PayTicketCommand request, CancellationToken cancellationToken)
    {
        var ticket = await _ticketsRepository.Get(request.Id, cancellationToken)
            ?? throw new InexistentTicketException($"Ticket with id {request.Id.Value} was not found");

        ticket.Pay();

        await _ticketsRepository.Update(ticket, cancellationToken);
    }
}
