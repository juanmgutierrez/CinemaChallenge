using Cinema.Application.Showtime.Repositories;
using Cinema.Domain.Auditorium.Entities;
using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Respositories;

internal class SeatsRepository : ISeatsRepository
{
    private readonly CinemaDbContext _context;

    public SeatsRepository(CinemaDbContext context)
    {
        _context = context;
    }

    public async Task<List<Seat>> GetSeatsInRowWithAuditorium(
        AuditoriumId auditoriumId,
        short row,
        short fromSeatNumber,
        short toSeatNumber,
        CancellationToken cancellationToken)
    {
        return await _context.Seats
            .Where(seat => seat.Auditorium.Id == auditoriumId && seat.Row == row && seat.SeatNumber >= fromSeatNumber && seat.SeatNumber <= toSeatNumber)
            .Include(seat => seat.Auditorium)
            .ToListAsync(cancellationToken);
    }
}
