using Cinema.Application.Auditorium.Repositories;
using Cinema.Application.Showtime.Repositories;
using Cinema.Domain.Auditorium.Exceptions;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.Exceptions;
using MediatR;

namespace Cinema.Application.Showtime.Commands.CreateShowtime;

internal sealed class CreateShowtimeCommandHandler : IRequestHandler<CreateShowtimeCommand, Domain.Showtime.Showtime>
{
    private readonly IShowtimesRepository _showtimesRepository;
    private readonly IAuditoriumsRepository _auditoriumsRepository;
    private readonly IMoviesRepository _moviesRepository;

    public CreateShowtimeCommandHandler(
        IShowtimesRepository showtimesRepository,
        IAuditoriumsRepository auditoriumsRepository,
        IMoviesRepository moviesRepository)
    {
        _showtimesRepository = showtimesRepository;
        _auditoriumsRepository = auditoriumsRepository;
        _moviesRepository = moviesRepository;
    }

    public async Task<Domain.Showtime.Showtime> Handle(CreateShowtimeCommand request, CancellationToken cancellationToken)
    {
        Movie? movie = request.MovieId is not null
            ? await _moviesRepository.GetByMovieId(request.MovieId, cancellationToken)
                ?? throw new InexistentMovieException($"Movie with id {request.MovieId.Value} was not found")
            : await _moviesRepository.GetByMovieImdbId(request.MovieImdbId!, cancellationToken)
                ?? throw new InexistentMovieException($"Movie with imdb id {request.MovieImdbId} was not found");

        var auditorium = await _auditoriumsRepository.Get(request.AuditoriumId, cancellationToken)
            ?? throw new InexistentAuditoriumException($"Auditorium with id {request.AuditoriumId} was not found");

        var showtime = Domain.Showtime.Showtime.Create(request.SessionDate, movie, auditorium);

        var dbShowtime = await _showtimesRepository.Add(showtime, cancellationToken);

        return dbShowtime;
    }
}
