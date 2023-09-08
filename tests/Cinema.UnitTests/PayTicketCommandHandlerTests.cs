using Cinema.Application.Showtime.Commands.CreateShowtime;
using Cinema.Application.Showtime.Commands.PayTicket;
using Cinema.Domain.Auditorium;
using Cinema.Domain.Auditorium.Exceptions;
using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Showtime;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.Exceptions;
using Cinema.Domain.Showtime.ValueObjects;
using Cinema.UnitTests.Fakes;
using CinemaAPI.Database.Repositories.Abstractions;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Cinema.UnitTests;

public class PayTicketCommandHandlerTests
{
    private readonly PayTicketCommandHandler _sut;

    private readonly ITicketsRepository _ticketsRepository = Substitute.For<ITicketsRepository>();

    public PayTicketCommandHandlerTests()
    {
        _sut = new PayTicketCommandHandler(_ticketsRepository);

    }

    //[Fact]
    //public void Handle_ShouldThrowInexistentMovieException_WhenMovieGivenByMovieIdDoesNotExist()
    //{
    //    // Arrange
    //    AuditoriumId auditoriumId = new(Guid.NewGuid());
    //    DateTimeOffset sessionDate = DateTimeOffset.UtcNow.AddDays(1);
    //    MovieId? movieId = new(Guid.NewGuid());
    //    string? movieImdbId = null;
    //    CreateShowtimeCommand command = new(auditoriumId, sessionDate, movieId, movieImdbId);
    //    CancellationToken cancellationToken = CancellationToken.None;

    //    _moviesRepository.GetByMovieId(movieId, cancellationToken).ReturnsNull();

    //    // Act
    //    var act = async () => await _sut.Handle(command, cancellationToken);

    //    // Assert
    //    act.Should().ThrowAsync<InexistentMovieException>()
    //        .WithMessage($"Movie with id {movieId} was not found");
    //}

    //[Fact]
    //public void Handle_ShouldThrowInexistentMovieException_WhenMovieGivenByMovieImdbIdDoesNotExist()
    //{
    //    // Arrange
    //    AuditoriumId auditoriumId = new(Guid.NewGuid());
    //    DateTimeOffset sessionDate = DateTimeOffset.UtcNow.AddDays(1);
    //    MovieId? movieId = null;
    //    string? movieImdbId = "tt1234567";
    //    CreateShowtimeCommand command = new(auditoriumId, sessionDate, movieId, movieImdbId);
    //    CancellationToken cancellationToken = CancellationToken.None;

    //    _moviesRepository.GetByMovieImdbId(movieImdbId, cancellationToken).ReturnsNull();

    //    // Act
    //    var act = async () => await _sut.Handle(command, cancellationToken);

    //    // Assert
    //    act.Should().ThrowAsync<InexistentMovieException>()
    //        .WithMessage($"Movie with id {movieId} was not found");
    //}

    //[Fact]
    //public void Handle_ShouldThrowInexistentAuditoriumException_WhenAuditoriumDoesNotExist()
    //{
    //    // Arrange
    //    AuditoriumId auditoriumId = new(Guid.NewGuid());
    //    DateTimeOffset sessionDate = DateTimeOffset.UtcNow.AddDays(1);
    //    MovieId? movieId = new(Guid.NewGuid());
    //    string? movieImdbId = "tt1234567";
    //    CreateShowtimeCommand command = new(auditoriumId, sessionDate, movieId, movieImdbId);
    //    CancellationToken cancellationToken = CancellationToken.None;

    //    _moviesRepository.GetByMovieId(movieId, cancellationToken).ReturnsNull();
    //    _moviesRepository.GetByMovieImdbId(movieImdbId, cancellationToken).ReturnsNull();
    //    _auditoriumsRepository.Get(auditoriumId, cancellationToken).ReturnsNull();

    //    // Act
    //    var act = async () => await _sut.Handle(command, cancellationToken);

    //    // Assert
    //    act.Should().ThrowAsync<InexistentAuditoriumException>()
    //        .WithMessage($"Auditorium with id {auditoriumId} was not found");
    //}

    //[Fact]
    //public async Task Handle_ShouldReturnNotNullShowtime_WhenValidAuditoriumAndMovieArePassed()
    //{
    //    // Arrange
    //    AuditoriumId auditoriumId = new(Guid.NewGuid());
    //    DateTimeOffset sessionDate = DateTimeOffset.UtcNow.AddDays(1);
    //    MovieId? movieId = new(Guid.NewGuid());
    //    string? movieImdbId = "tt1234567";
    //    CreateShowtimeCommand command = new(auditoriumId, sessionDate, movieId, movieImdbId);
    //    CancellationToken cancellationToken = CancellationToken.None;

    //    _moviesRepository.GetByMovieId(movieId, cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
    //    _moviesRepository.GetByMovieImdbId(movieImdbId, cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
    //    _auditoriumsRepository.Get(auditoriumId, cancellationToken).Returns(Task.FromResult<Auditorium?>(AuditoriumFakes.ValidAuditorium));
    //    _showtimesRepository.Add(Arg.Any<Showtime>(), cancellationToken).Returns(Task.FromResult(ShowtimeFakes.ValidShowtime));

    //    // Act
    //    var showtime = await _sut.Handle(command, cancellationToken);

    //    // Assert
    //    showtime.Should().NotBeNull();
    //    showtime.Should().BeOfType<Showtime>();
    //}

    //[Fact]
    //public async Task Handle_ShouldReturnShowtimeWithSameMovie_WhenValidAuditoriumAndMovieArePassed()
    //{
    //    // Arrange
    //    AuditoriumId auditoriumId = new(Guid.NewGuid());
    //    DateTimeOffset sessionDate = DateTimeOffset.UtcNow.AddDays(1);
    //    MovieId? movieId = new(Guid.NewGuid());
    //    string? movieImdbId = "tt1234567";
    //    CreateShowtimeCommand command = new(auditoriumId, sessionDate, movieId, movieImdbId);
    //    CancellationToken cancellationToken = CancellationToken.None;

    //    _moviesRepository.GetByMovieId(movieId, cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
    //    _moviesRepository.GetByMovieImdbId(movieImdbId, cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
    //    _auditoriumsRepository.Get(auditoriumId, cancellationToken).Returns(Task.FromResult<Auditorium?>(AuditoriumFakes.ValidAuditorium));
    //    _showtimesRepository.Add(Arg.Any<Showtime>(), cancellationToken).Returns(Task.FromResult(ShowtimeFakes.ValidShowtime));

    //    // Act
    //    var showtime = await _sut.Handle(command, cancellationToken);

    //    // Assert
    //    showtime.Movie.Id.Equals(movieId);
    //}

    //[Fact]
    //public async Task Handle_ShouldReturnShowtimeWithSameAuditorium_WhenValidAuditoriumAndMovieArePassed()
    //{
    //    // Arrange
    //    AuditoriumId auditoriumId = new(Guid.NewGuid());
    //    DateTimeOffset sessionDate = DateTimeOffset.UtcNow.AddDays(1);
    //    MovieId? movieId = new(Guid.NewGuid());
    //    string? movieImdbId = "tt1234567";
    //    CreateShowtimeCommand command = new(auditoriumId, sessionDate, movieId, movieImdbId);
    //    CancellationToken cancellationToken = CancellationToken.None;

    //    _moviesRepository.GetByMovieId(movieId, cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
    //    _moviesRepository.GetByMovieImdbId(movieImdbId, cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
    //    _auditoriumsRepository.Get(auditoriumId, cancellationToken).Returns(Task.FromResult<Auditorium?>(AuditoriumFakes.ValidAuditorium));
    //    _showtimesRepository.Add(Arg.Any<Showtime>(), cancellationToken).Returns(Task.FromResult(ShowtimeFakes.ValidShowtime));

    //    // Act
    //    var showtime = await _sut.Handle(command, cancellationToken);

    //    // Assert
    //    showtime.Auditorium.Id.Equals(auditoriumId);
    //}

    //[Fact]
    //public async Task Handle_ShouldReturnShowtimeWithDesiredSessionDate_WhenValidAuditoriumAndMovieArePassed()
    //{
    //    // Arrange
    //    AuditoriumId auditoriumId = new(Guid.NewGuid());
    //    DateTimeOffset sessionDate = DateTimeOffset.UtcNow.AddDays(1);
    //    MovieId? movieId = new(Guid.NewGuid());
    //    string? movieImdbId = "tt1234567";
    //    CreateShowtimeCommand command = new(auditoriumId, sessionDate, movieId, movieImdbId);
    //    CancellationToken cancellationToken = CancellationToken.None;

    //    _moviesRepository.GetByMovieId(movieId, cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
    //    _moviesRepository.GetByMovieImdbId(movieImdbId, cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
    //    _auditoriumsRepository.Get(auditoriumId, cancellationToken).Returns(Task.FromResult<Auditorium?>(AuditoriumFakes.ValidAuditorium));
    //    _showtimesRepository.Add(Arg.Any<Showtime>(), cancellationToken).Returns(Task.FromResult(ShowtimeFakes.ValidShowtime));

    //    // Act
    //    var showtime = await _sut.Handle(command, cancellationToken);

    //    // Assert
    //    showtime.SessionDate.Equals(sessionDate);
    //}

    //[Fact]
    //public async Task Handle_ShouldReturnShowtimeWithoutTickets_WhenValidAuditoriumAndMovieArePassed()
    //{
    //    // Arrange
    //    AuditoriumId auditoriumId = new(Guid.NewGuid());
    //    DateTimeOffset sessionDate = DateTimeOffset.UtcNow.AddDays(1);
    //    MovieId? movieId = new(Guid.NewGuid());
    //    string? movieImdbId = "tt1234567";
    //    CreateShowtimeCommand command = new(auditoriumId, sessionDate, movieId, movieImdbId);
    //    CancellationToken cancellationToken = CancellationToken.None;

    //    _moviesRepository.GetByMovieId(movieId, cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
    //    _moviesRepository.GetByMovieImdbId(movieImdbId, cancellationToken).Returns(Task.FromResult<Movie?>(MovieFakes.ValidMovie));
    //    _auditoriumsRepository.Get(auditoriumId, cancellationToken).Returns(Task.FromResult<Auditorium?>(AuditoriumFakes.ValidAuditorium));
    //    _showtimesRepository.Add(Arg.Any<Showtime>(), cancellationToken).Returns(Task.FromResult(ShowtimeFakes.ValidShowtime));

    //    // Act
    //    var showtime = await _sut.Handle(command, cancellationToken);

    //    // Assert
    //    showtime.Tickets.Should().BeEmpty();
    //}
}
