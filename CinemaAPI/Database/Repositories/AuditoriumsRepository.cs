using CinemaAPI.Database.Entities;
using CinemaAPI.Database.Repositories.Abstractions;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Database.Repositories;

public class AuditoriumsRepository : IAuditoriumsRepository
{
    private readonly CinemaContext _context;

    public AuditoriumsRepository(CinemaContext context) => _context = context;

    public async Task<AuditoriumEntity?> Get(int auditoriumId, CancellationToken cancellationToken) => await _context.Auditoriums
            .Include(x => x.Seats)
            .FirstOrDefaultAsync(x => x.Id == auditoriumId, cancellationToken);
}
