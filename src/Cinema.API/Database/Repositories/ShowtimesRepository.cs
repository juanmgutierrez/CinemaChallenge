using CinemaAPI.Database.Entities;
using CinemaAPI.Database.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace CinemaAPI.Database.Repositories;

public class ShowtimesRepository : IShowtimesRepository
{
    private readonly CinemaContext _context;

    public ShowtimesRepository(CinemaContext context) => _context = context;

    public async Task<ShowtimeEntity?> Get(
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

        return await query.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IEnumerable<ShowtimeEntity>> GetAll(
        Expression<Func<ShowtimeEntity, bool>> filter,
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

    public async Task<ShowtimeEntity> CreateShowtime(ShowtimeEntity showtimeEntity, CancellationToken cancellationToken)
    {
        var showtime = _context.Showtimes.Add(showtimeEntity);
        await _context.SaveChangesAsync(cancellationToken);
        return showtime.Entity;
    }
}
