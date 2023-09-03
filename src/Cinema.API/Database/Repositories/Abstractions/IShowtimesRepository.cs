using CinemaAPI.Database.Entities;
using System.Linq.Expressions;

namespace CinemaAPI.Database.Repositories.Abstractions;

public interface IShowtimesRepository
{
    Task<ShowtimeEntity?> Get(int id, CancellationToken cancellationToken, bool includeMovie = false, bool includeTickets = false);

    Task<IEnumerable<ShowtimeEntity>> GetAll(
        Expression<Func<ShowtimeEntity, bool>> filter,
        CancellationToken cancellationToken,
        bool includeMovie = false,
        bool includeTickets = false);

    Task<ShowtimeEntity> CreateShowtime(ShowtimeEntity showtimeEntity, CancellationToken cancellationToken);
}
