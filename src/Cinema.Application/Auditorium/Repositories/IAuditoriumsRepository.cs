using Cinema.Domain.Auditorium.ValueObjects;

namespace Cinema.Application.Auditorium.Repositories;

public interface IAuditoriumsRepository
{
    Task<Domain.Auditorium.Auditorium?> Get(AuditoriumId auditoriumId, CancellationToken cancellationToken);
}
