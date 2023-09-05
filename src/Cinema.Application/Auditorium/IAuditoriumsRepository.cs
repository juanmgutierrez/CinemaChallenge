using Cinema.Domain.Auditorium;
using Cinema.Domain.Auditorium.ValueObjects;

namespace CinemaAPI.Database.Repositories.Abstractions;

public interface IAuditoriumsRepository
{
    Task<Auditorium?> Get(AuditoriumId auditoriumId, CancellationToken cancellationToken);
}
