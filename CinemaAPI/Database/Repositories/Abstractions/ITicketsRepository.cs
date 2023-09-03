using CinemaAPI.Database.Entities;

namespace CinemaAPI.Database.Repositories.Abstractions;

public interface ITicketsRepository
{
    Task<TicketEntity?> Get(Guid id, CancellationToken cancellationToken, bool includeShowtime = false, bool includeSeats = false);

    Task<TicketEntity> Create(ShowtimeEntity showtime, ICollection<SeatEntity> selectedSeats, CancellationToken cancellationToken);

    Task<TicketEntity> ConfirmPayment(TicketEntity ticket, CancellationToken cancellationToken);
}
