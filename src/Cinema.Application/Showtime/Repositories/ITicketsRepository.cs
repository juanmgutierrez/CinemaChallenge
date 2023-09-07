using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.ValueObjects;

namespace CinemaAPI.Database.Repositories.Abstractions;

public interface ITicketsRepository
{
    Task<Ticket?> Get(TicketId id, CancellationToken cancellationToken);

    Task<Ticket> Add(Ticket ticket, CancellationToken cancellationToken);

    Task<Ticket> Update(Ticket ticket, CancellationToken cancellationToken);
}
