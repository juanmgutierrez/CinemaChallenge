using Cinema.Application.Auditorium.Repositories;
using Cinema.Application.Common.Proxies;
using Cinema.Application.Showtime.Repositories;
using Cinema.Domain.Auditorium.Exceptions;
using Cinema.Domain.Showtime.Exceptions;
using MediatR;

namespace Cinema.Application.Showtime.Commands.CreateShowtime;

internal sealed class CreateShowtimeCommandHandler : IRequestHandler<CreateShowtimeCommand, Domain.Showtime.Showtime>
{
    private readonly IShowtimesRepository _showtimesRepository;
    private readonly IAuditoriumsRepository _auditoriumsRepository;
    private readonly IMoviesAPIProxy _moviesAPIProxy;

    public CreateShowtimeCommandHandler(
        IShowtimesRepository showtimesRepository,
        IAuditoriumsRepository auditoriumsRepository,
        IMoviesAPIProxy moviesAPIProxy)
    {
        _showtimesRepository = showtimesRepository;
        _auditoriumsRepository = auditoriumsRepository;
        _moviesAPIProxy = moviesAPIProxy;
    }

    public async Task<Domain.Showtime.Showtime> Handle(CreateShowtimeCommand request, CancellationToken cancellationToken)
    {
        var movie = await _moviesAPIProxy.GetMovie(request.MovieImdbId, cancellationToken)
            ?? throw new InexistentMovieException($"Movie with id {request.MovieImdbId} was not found");

        var auditorium = await _auditoriumsRepository.Get(request.AuditoriumId, cancellationToken)
            ?? throw new InexistentAuditoriumException($"Auditorium with id {request.AuditoriumId} was not found");

        // Showtime ID is created in the database, 1 is a default
        var showtime = Domain.Showtime.Showtime.Create(new Domain.Showtime.ValueObjects.ShowtimeId(1), request.SessionDate, movie, auditorium);

        var dbShowtime = await _showtimesRepository.Add(showtime, cancellationToken);

        return dbShowtime;
    }
}
