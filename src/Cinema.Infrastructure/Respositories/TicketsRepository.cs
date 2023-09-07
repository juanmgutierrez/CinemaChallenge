using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.ValueObjects;
using Cinema.Infrastructure.Contexts;
using CinemaAPI.Database.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Respositories;

public class TicketsRepository : ITicketsRepository
{
    private readonly CinemaDbContext _context;

    public TicketsRepository(CinemaDbContext context) => _context = context;

    public async Task<Ticket?> Get(TicketId id, CancellationToken cancellationToken)
    {
        return await _context.Tickets.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<Ticket> Add(Ticket ticket, CancellationToken cancellationToken)
    {
        //_context.ChangeTracker.TrackGraph(
        //    ticket,
        //    node => node.Entry.State = EntityState.Unchanged);

        _context.Tickets.Add(ticket);

        // TODO Implement UoW and remove this
        await _context.SaveChangesAsync(cancellationToken);

        return await _context.Tickets
            .Include(t => t.Showtime)
            .Include(t => t.Seats)
            .FirstAsync(t => t.Id == ticket.Id, cancellationToken);
    }

    public async Task<Ticket> Update(Ticket ticket, CancellationToken cancellationToken)
    {
        var entityEntry = _context.Tickets.Update(ticket);
        // TODO Implement UoW and remove this
        await _context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }
}
