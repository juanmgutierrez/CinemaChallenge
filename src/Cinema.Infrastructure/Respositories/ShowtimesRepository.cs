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
            .Include(showtime => showtime.Auditorium)
            .Include(showtime => showtime.Movie)
            .FirstOrDefaultAsync(showtime => showtime.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<Showtime>> GetAll(
        Expression<Func<Showtime, bool>> filter,
        CancellationToken cancellationToken)
    {
        if (filter is null)
            return Array.Empty<Showtime>();

        return await _context.Showtimes
            .Include(showtime => showtime.Auditorium)
            .Include(showtime => showtime.Movie)
            .Include(showtime => showtime.Tickets)
            .Where(filter)
            .ToListAsync();
    }

    public async Task<Showtime> Add(Showtime showtime, CancellationToken cancellationToken)
    {
        var entityEntry = _context.Showtimes.Add(showtime);
        // TODO Implement UoW and remove this
        await _context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }
}
