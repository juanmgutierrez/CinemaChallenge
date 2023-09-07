using System.Linq.Expressions;

namespace Cinema.Application.Showtime.Repositories;

public interface IShowtimesRepository
{
    // TODO Improve namespaces and entities names
    Task<Domain.Showtime.Showtime?> Get(int id, CancellationToken cancellationToken, bool includeMovie = false, bool includeTickets = false);

    Task<IEnumerable<Domain.Showtime.Showtime>> GetAll(
        Expression<Func<Domain.Showtime.Showtime, bool>> filter,
        CancellationToken cancellationToken,
        bool includeMovie = false,
        bool includeTickets = false);

    Task<Domain.Showtime.Showtime> Add(Domain.Showtime.Showtime showtime, CancellationToken cancellationToken);
}
