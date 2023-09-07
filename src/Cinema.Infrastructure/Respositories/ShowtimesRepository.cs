using Cinema.Application.Showtime.Repositories;
using Cinema.Domain.Showtime;
using Cinema.Domain.Showtime.ValueObjects;
using Cinema.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cinema.Infrastructure.Respositories;

public class ShowtimesRepository : IShowtimesRepository
{
    private readonly CinemaDbContext _context;

    public ShowtimesRepository(CinemaDbContext context) => _context = context;

    public async Task<Showtime?> GetWithAuditoriumMovieAndTickets(
        ShowtimeId id,
        CancellationToken cancellationToken)
    {
        return await _context.Showtimes
            .Include(show => show.Auditorium)
            .Include(show => show.Movie)
            .Include(show => show.Tickets)
            .ThenInclude(ticket => ticket.Seats)
            .FirstOrDefaultAsync(show => show.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Showtime>> GetAll(
        Expression<Func<Showtime, bool>> filter,
        CancellationToken cancellationToken)
    {
        if (filter is null)
            return Array.Empty<Showtime>();

        return await _context.Showtimes
            .Include(show => show.Auditorium)
            .Include(show => show.Movie)
            .Include(show => show.Tickets)
            .ThenInclude(ticket => ticket.Seats)
            .Where(filter)
            .ToListAsync(cancellationToken);
    }

    public async Task<Showtime> Add(Showtime showtime, CancellationToken cancellationToken)
    {
        _context.ChangeTracker.TrackGraph(
            showtime,
            node => node.Entry.State = EntityState.Unchanged);

        var addedShowtime = _context.Showtimes.Add(showtime);

        // TODO Implement UoW and remove this
        await _context.SaveChangesAsync(cancellationToken);

        return await _context.Showtimes
            .Include(show => show.Movie)
            .Include(show => show.Auditorium)
            .Include(show => show.Tickets)
            .ThenInclude(ticket => ticket.Seats)
            .FirstAsync(show => show.Id == showtime.Id, cancellationToken);
    }
}
