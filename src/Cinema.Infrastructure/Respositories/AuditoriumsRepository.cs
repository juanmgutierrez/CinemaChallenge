using Cinema.Application.Auditorium.Repositories;
using Cinema.Domain.Auditorium;
using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Infrastructure.Contexts;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Respositories;

public class AuditoriumsRepository : IAuditoriumsRepository
{
    private readonly CinemaDbContext _context;

    public AuditoriumsRepository(CinemaDbContext context) => _context = context;

    public async Task<Auditorium?> Get(AuditoriumId auditoriumId, CancellationToken cancellationToken) =>
        await _context.Auditoriums
            .FirstOrDefaultAsync(auditorium => auditorium.Id == auditoriumId, cancellationToken);
}
