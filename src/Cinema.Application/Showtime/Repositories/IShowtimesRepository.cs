using Cinema.Domain.Showtime.ValueObjects;
using System.Linq.Expressions;

namespace Cinema.Application.Showtime.Repositories;

public interface IShowtimesRepository
{
    // TODO Improve namespaces and entities names
    Task<Domain.Showtime.Showtime?> GetWithAuditoriumMovieAndTickets(ShowtimeId id, CancellationToken cancellationToken);

    Task<IEnumerable<Domain.Showtime.Showtime>> GetAll(
        Expression<Func<Domain.Showtime.Showtime, bool>> filter,
        CancellationToken cancellationToken);

    Task<Domain.Showtime.Showtime> Add(Domain.Showtime.Showtime showtime, CancellationToken cancellationToken);
}
