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
        return await GetShowtimesQueryableWithIncludes()!.FirstOrDefaultAsync(x => x.Id.Value == id, cancellationToken);
    }

    public async Task<IEnumerable<Showtime>> GetAll(
        Expression<Func<Showtime, bool>> filter,
        CancellationToken cancellationToken,
        bool includeMovie = false,
        bool includeTickets = false)
    {
        return await GetShowtimesQueryableWithIncludes()!.ToListAsync(cancellationToken);
    }

    public async Task<Showtime> Add(Showtime showtime, CancellationToken cancellationToken)
    {
        var entityEntry = _context.Showtimes.Add(showtime);
        // TODO Implement UoW and remove this
        await _context.SaveChangesAsync(cancellationToken);
        return entityEntry.Entity;
    }

    private IQueryable<Showtime>? GetShowtimesQueryableWithIncludes(bool includeMovie = false, bool includeTickets = false)
    {
        var query = _context.Showtimes.AsQueryable();

        if (includeMovie)
            query = query.Include(x => x.Movie);
        if (includeTickets)
            query = query.Include(x => x.Tickets);

        return query;
    }
}
