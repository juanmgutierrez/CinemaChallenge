using CinemaAPI.Database.Entities;
using CinemaAPI.Database.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Database.Repositories
{
    public class TicketsRepository : ITicketsRepository
    {
        private readonly CinemaContext _context;

        public TicketsRepository(CinemaContext context) => _context = context;

        public async Task<TicketEntity?> Get(Guid id, CancellationToken cancellationToken, bool includeShowtime = false, bool includeSeats = false)
        {
            var query = _context.Tickets.AsQueryable();

            if (includeShowtime)
                query = query.Include(x => x.Showtime);
            if (includeSeats)
                query = query.Include(x => x.Seats);

            return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<TicketEntity> Create(ShowtimeEntity showtime, ICollection<SeatEntity> selectedSeats, CancellationToken cancellationToken)
        {
            var ticket = TicketEntity.Create(showtime.Id, selectedSeats);

            var createdTicket = _context.Tickets.Add(ticket);

            await _context.SaveChangesAsync(cancellationToken);

            return createdTicket.Entity;
        }

        public async Task<TicketEntity> ConfirmPayment(TicketEntity ticket, CancellationToken cancellationToken)
        {
            ticket.Pay();

            _context.Update(ticket);

            await _context.SaveChangesAsync(cancellationToken);

            return ticket;
        }
    }
}
