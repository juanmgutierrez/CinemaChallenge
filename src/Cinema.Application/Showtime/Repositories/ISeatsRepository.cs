using Cinema.Domain.Auditorium.Entities;
using Cinema.Domain.Auditorium.ValueObjects;

namespace Cinema.Application.Showtime.Repositories;

public interface ISeatsRepository
{
    Task<List<Seat>> GetSeatsInRowWithAuditorium(
        AuditoriumId auditoriumId,
        short row,
        short fromSeatNumber,
        short toSeatNumber,
        CancellationToken cancellationToken);
}
