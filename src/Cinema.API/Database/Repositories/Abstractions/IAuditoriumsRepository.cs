using CinemaAPI.Database.Entities;

namespace CinemaAPI.Database.Repositories.Abstractions;

public interface IAuditoriumsRepository
{
    Task<AuditoriumEntity?> Get(int auditoriumId, CancellationToken cancellationToken);
}
