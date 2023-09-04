using Cinema.Application.Showtime.Repositories;
using Cinema.Domain.Showtime;
using Cinema.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace Cinema.Infrastructure.Respositories;

public class ShowtimesRepository : IShowtimesRepository
{
    private readonly CinemaDbContext _context;

    public ShowtimesRepository(CinemaDbContext context) => _context = context;

    public async Task<Showtime?> Get(
        int id,
        CancellationToken cancellationToken,
        bool includeMovie = false,
        bool includeTickets = false)
    {
        var query = _context.Showtimes.AsQueryable();

        if (includeMovie)
            query = query.Include(x => x.Movie);
        if (includeTickets)
            query = query.Include(x => x.Tickets);

        return await query.FirstOrDefaultAsync(x => x.Id.Value == id, cancellationToken);
    }

    public async Task<IEnumerable<Showtime>> GetAll(
        Expression<Func<Showtime, bool>> filter,
        CancellationToken cancellationToken,
        bool includeMovie = false,
        bool includeTickets = false)
    {
        var query = _context.Showtimes.AsQueryable();

        if (includeMovie)
            query = query.Include(x => x.Movie);
        if (includeTickets)
            query = query.Include(x => x.Tickets);
        if (filter is not null)
            query = query.Where(filter);

        return await query.ToListAsync(cancellationToken);
    }

    public async Task<Showtime> CreateShowtime(Showtime showtimeEntity, CancellationToken cancellationToken)
    {
        var showtime = _context.Showtimes.Add(showtimeEntity);
        await _context.SaveChangesAsync(cancellationToken);
        return showtime.Entity;
    }
}
