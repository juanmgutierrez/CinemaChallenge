using Cinema.Application.Auditorium.Repositories;
using Cinema.Application.Showtime.Commands.CreateShowtime;
using Cinema.Application.Showtime.Repositories;
using Cinema.Domain.Auditorium;
using Cinema.Domain.Auditorium.Exceptions;
using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Showtime;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.Exceptions;
using Cinema.Domain.Showtime.ValueObjects;
using Cinema.UnitTests.Fakes;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Cinema.UnitTests;

public class CreateShowtimeCommandHandlerTests
{
    private readonly CreateShowtimeCommandHandler _sut;

    private readonly IShowtimesRepository _showtimesRepository = Substitute.For<IShowtimesRepository>();
    private readonly IAuditoriumsRepository _auditoriumsRepository = Substitute.For<IAuditoriumsRepository>();
    private readonly IMoviesRepository _moviesRepository = Substitute.For<IMoviesRepository>();

    private readonly AuditoriumId _auditoriumId = new(FakeConstants.AuditoriumId);
    private readonly DateTimeOffset _sessionDate = DateTimeOffset.UtcNow.AddDays(1);
    private readonly MovieId _movieId = new(FakeConstants.MovieId);
    private readonly string _movieImdbId = FakeConstants.MovieImdbId;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    public CreateShowtimeCommandHandlerTests()
    {
        _sut = new CreateShowtimeCommandHandler(
            _showtimesRepository,
            _auditoriumsRepository,
            _moviesRepository);
    }

    [Fact]
    public void Handle_ShouldThrowInexistentMovieException_WhenMovieGivenByMovieIdDoesNotExist()
    {
        // Arrange
        string? movieImdbId = null;
        CreateShowtimeCommand command = new(_auditoriumId, _sessionDate, _movieId, movieImdbId);

        _moviesRepository.GetByMovieId(_movieId, _cancellationToken).ReturnsNull();

        // Act
        var act = async () => await _sut.Handle(command, _cancellationToken);

        // Assert
        act.Should().ThrowAsync<InexistentMovieException>()
            .WithMessage($"Movie with id {_movieId.Value} was not found");
    }

    [Fact]
    public void Handle_ShouldThrowInexistentMovieException_WhenMovieGivenByMovieImdbIdDoesNotExist()
    {
        // Arrange
        MovieId? movieId = null;
        CreateShowtimeCommand command = new(_auditoriumId, _sessionDate, movieId, _movieImdbId);

        _moviesRepository.GetByMovieImdbId(_movieImdbId, _cancellationToken).ReturnsNull();

        // Act
        var act = async () => await _sut.Handle(command, _cancellationToken);

        // Assert
        act.Should().ThrowAsync<InexistentMovieException>()
            .WithMessage($"Movie with imdb id {_movieImdbId} was not found");
    }

    [Fact]
    public void Handle_ShouldThrowInexistentAuditoriumException_WhenAuditoriumDoesNotExist()
    {
        // Arrange
        CreateShowtimeCommand command = new(_auditoriumId, _sessionDate, _movieId, _movieImdbId);

        _moviesRepository.GetByMovieId(_movieId, _cancellationToken).ReturnsNull();
        _moviesRepository.GetByMovieImdbId(_movieImdbId, _cancellationToken).ReturnsNull();
        _auditoriumsRepository.Get(_auditoriumId, _cancellationToken).ReturnsNull();

        // Act
        var act = async () => await _sut.Handle(command, _cancellationToken);

        // Assert
        act.Should().ThrowAsync<InexistentAuditoriumException>()
            .WithMessage($"Auditorium with id {_auditoriumId} was not found");
    }

    [Fact]
    public async Task Handle_ShouldReturnNotNullShowtime_WhenValidAuditoriumAndMovieArePassed()
    {
        // Arrange
        CreateShowtimeCommand command = new(_auditoriumId, _sessionDate, _movieId, _movieImdbId);

        _moviesRepository.GetByMovieId(_movieId, _cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
        _moviesRepository.GetByMovieImdbId(_movieImdbId, _cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
        _auditoriumsRepository.Get(_auditoriumId, _cancellationToken).Returns(Task.FromResult<Auditorium?>(AuditoriumFakes.ValidAuditorium));
        _showtimesRepository.Add(Arg.Any<Showtime>(), _cancellationToken).Returns(Task.FromResult(ShowtimeFakes.ValidShowtime(_sessionDate)));

        // Act
        var showtime = await _sut.Handle(command, _cancellationToken);

        // Assert
        showtime.Should().NotBeNull();
        showtime.Should().BeOfType<Showtime>();
    }

    [Fact]
    public async Task Handle_ShouldReturnShowtimeWithSameMovie_WhenValidAuditoriumAndMovieArePassed()
    {
        // Arrange
        CreateShowtimeCommand command = new(_auditoriumId, _sessionDate, _movieId, _movieImdbId);

        _moviesRepository.GetByMovieId(_movieId, _cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
        _moviesRepository.GetByMovieImdbId(_movieImdbId, _cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
        _auditoriumsRepository.Get(_auditoriumId, _cancellationToken).Returns(Task.FromResult<Auditorium?>(AuditoriumFakes.ValidAuditorium));
        _showtimesRepository.Add(Arg.Any<Showtime>(), _cancellationToken).Returns(Task.FromResult(ShowtimeFakes.ValidShowtime(_sessionDate)));

        // Act
        var showtime = await _sut.Handle(command, _cancellationToken);

        // Assert
        showtime.Movie.Id.Should().Be(_movieId);
    }

    [Fact]
    public async Task Handle_ShouldReturnShowtimeWithSameAuditorium_WhenValidAuditoriumAndMovieArePassed()
    {
        // Arrange
        CreateShowtimeCommand command = new(_auditoriumId, _sessionDate, _movieId, _movieImdbId);

        _moviesRepository.GetByMovieId(_movieId, _cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
        _moviesRepository.GetByMovieImdbId(_movieImdbId, _cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
        _auditoriumsRepository.Get(_auditoriumId, _cancellationToken).Returns(Task.FromResult<Auditorium?>(AuditoriumFakes.ValidAuditorium));
        _showtimesRepository.Add(Arg.Any<Showtime>(), _cancellationToken).Returns(Task.FromResult(ShowtimeFakes.ValidShowtime(_sessionDate)));

        // Act
        var showtime = await _sut.Handle(command, _cancellationToken);

        // Assert
        showtime.Auditorium.Id.Should().Be(_auditoriumId);
    }

    [Fact]
    public async Task Handle_ShouldReturnShowtimeWithDesiredSessionDate_WhenValidAuditoriumAndMovieArePassed()
    {
        // Arrange
        CreateShowtimeCommand command = new(_auditoriumId, _sessionDate, _movieId, _movieImdbId);

        _moviesRepository.GetByMovieId(_movieId, _cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
        _moviesRepository.GetByMovieImdbId(_movieImdbId, _cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
        _auditoriumsRepository.Get(_auditoriumId, _cancellationToken).Returns(Task.FromResult<Auditorium?>(AuditoriumFakes.ValidAuditorium));
        _showtimesRepository.Add(Arg.Any<Showtime>(), _cancellationToken).Returns(Task.FromResult(ShowtimeFakes.ValidShowtime(_sessionDate)));

        // Act
        var showtime = await _sut.Handle(command, _cancellationToken);

        // Assert
        showtime.SessionDate.Should().Be(_sessionDate);
    }

    [Fact]
    public async Task Handle_ShouldReturnShowtimeWithoutTickets_WhenValidAuditoriumAndMovieArePassed()
    {
        // Arrange
        CreateShowtimeCommand command = new(_auditoriumId, _sessionDate, _movieId, _movieImdbId);

        _moviesRepository.GetByMovieId(_movieId, _cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
        _moviesRepository.GetByMovieImdbId(_movieImdbId, _cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
        _auditoriumsRepository.Get(_auditoriumId, _cancellationToken).Returns(Task.FromResult<Auditorium?>(AuditoriumFakes.ValidAuditorium));
        _showtimesRepository.Add(Arg.Any<Showtime>(), _cancellationToken).Returns(Task.FromResult(ShowtimeFakes.ValidShowtime(_sessionDate)));

        // Act
        var showtime = await _sut.Handle(command, _cancellationToken);

        // Assert
        showtime.Tickets.Should().BeEmpty();
    }
}
