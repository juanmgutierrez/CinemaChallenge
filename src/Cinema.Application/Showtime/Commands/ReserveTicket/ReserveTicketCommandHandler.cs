using Cinema.Application.Showtime.Repositories;
using Cinema.Domain.Auditorium.Entities;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.Exceptions;
using CinemaAPI.Database.Repositories.Abstractions;
using MediatR;

namespace Cinema.Application.Showtime.Commands.ReserveTicket;

internal sealed class ReserveTicketCommandHandler : IRequestHandler<ReserveTicketCommand, Ticket>
{
    private readonly IShowtimesRepository _showtimesRepository;
    private readonly ISeatsRepository _seatsRepository;
    private readonly ITicketsRepository _ticketsRepository;

    public ReserveTicketCommandHandler(
        IShowtimesRepository showtimesRepository,
        ISeatsRepository seatsRepository,
        ITicketsRepository ticketsRepository)
    {
        _showtimesRepository = showtimesRepository;
        _seatsRepository = seatsRepository;
        _ticketsRepository = ticketsRepository;
    }

    public async Task<Ticket> Handle(ReserveTicketCommand request, CancellationToken cancellationToken)
    {
        var showtime = await _showtimesRepository.GetWithAuditoriumMovieAndTickets(request.ShowtimeId, cancellationToken)
            ?? throw new InexistentShowtimeException($"Showtime with id {request.ShowtimeId.Value} was not found");

        List<Seat> seats = await _seatsRepository.GetSeatsInRowWithAuditorium(
            showtime.Auditorium.Id,
            request.Row,
            request.FromSeatNumber,
            request.ToSeatNumber,
            cancellationToken);

        var ticket = Ticket.Create(showtime, seats);

        return await _ticketsRepository.Add(ticket, cancellationToken);
    }
}
