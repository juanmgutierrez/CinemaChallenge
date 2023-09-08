using Cinema.Application.Showtime.Commands.ReserveTicket;
using Cinema.Application.Showtime.Repositories;
using Cinema.Domain.Auditorium.Entities;
using Cinema.Domain.Showtime;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.Exceptions;
using Cinema.UnitTests.Fakes;
using CinemaAPI.Database.Repositories.Abstractions;
using FluentAssertions;
using NSubstitute;
using NSubstitute.ReturnsExtensions;

namespace Cinema.UnitTests;

public class ReserveTicketCommandHandlerTests
{
    private readonly ReserveTicketCommandHandler _sut;

    private readonly IShowtimesRepository _showtimesRepository = Substitute.For<IShowtimesRepository>();
    private readonly ISeatsRepository _seatsRepository = Substitute.For<ISeatsRepository>();
    private readonly ITicketsRepository _ticketsRepository = Substitute.For<ITicketsRepository>();

    private readonly DateTimeOffset _sessionDate;
    private readonly Showtime _validShowtime;
    private readonly List<Seat> _validSeatsList;
    private readonly Ticket _validTicket;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    public ReserveTicketCommandHandlerTests()
    {
        _sut = new ReserveTicketCommandHandler(
            _showtimesRepository,
            _seatsRepository,
            _ticketsRepository);

        _sessionDate = DateTimeOffset.UtcNow.AddDays(1);
        _validShowtime = ShowtimeFakes.ValidShowtime(_sessionDate);
        _validSeatsList = SeatFakes.ValidSeatsList(
            FakeConstants.SeatsRow,
            FakeConstants.SeatsFromSeatNumber,
            FakeConstants.SeatsUntilSeatNumber);
        _validTicket = TicketFakes.ValidTicket(
            _validShowtime,
            _validSeatsList);
    }

    [Fact]
    public void Handle_ShouldThrowInexistentShowtimeException_WhenShowtimeIdDoesNotExist()
    {
        // Arrange
        ReserveTicketCommand command = new(
            _validShowtime.Id,
            FakeConstants.SeatsRow,
            FakeConstants.SeatsFromSeatNumber,
            FakeConstants.SeatsUntilSeatNumber);

        _showtimesRepository.GetWithAuditoriumMovieAndTickets(_validShowtime.Id, _cancellationToken).ReturnsNull();

        // Act
        var act = async () => await _sut.Handle(command, _cancellationToken);

        // Assert
        act.Should().ThrowAsync<InexistentShowtimeException>()
            .WithMessage($"Showtime with id {_validShowtime.Id} was not found");
    }

    [Fact]
    public async Task Handle_ShouldReturnNotNullTicket_WhenValidCommandIsPassed()
    {
        // Arrange
        ReserveTicketCommand command = new(
            _validShowtime.Id,
            FakeConstants.SeatsRow,
            FakeConstants.SeatsFromSeatNumber,
            FakeConstants.SeatsUntilSeatNumber);

        _showtimesRepository.GetWithAuditoriumMovieAndTickets(_validShowtime.Id, _cancellationToken)
            .Returns(Task.FromResult<Showtime?>(_validShowtime));
        _seatsRepository.GetSeatsInRowWithAuditorium(
            _validShowtime.Auditorium.Id,
            FakeConstants.SeatsRow,
            FakeConstants.SeatsFromSeatNumber,
            FakeConstants.SeatsUntilSeatNumber,
            _cancellationToken)
            .Returns(_validSeatsList);
        _ticketsRepository.Add(Arg.Any<Ticket>(), _cancellationToken)
            .Returns(_validTicket);

        // Act
        var ticket = await _sut.Handle(command, _cancellationToken);

        // Assert
        ticket.Should().NotBeNull();
        ticket.Should().BeOfType<Ticket>();
    }

    [Fact]
    public async Task Handle_ShouldReturnTicketWithSelectedShowtime_WhenValidCommandIsPassed()
    {
        // Arrange
        ReserveTicketCommand command = new(
            _validShowtime.Id,
            FakeConstants.SeatsRow,
            FakeConstants.SeatsFromSeatNumber,
            FakeConstants.SeatsUntilSeatNumber);

        _showtimesRepository.GetWithAuditoriumMovieAndTickets(_validShowtime.Id, _cancellationToken)
            .Returns(Task.FromResult<Showtime?>(_validShowtime));
        _seatsRepository.GetSeatsInRowWithAuditorium(
            _validShowtime.Auditorium.Id,
            FakeConstants.SeatsRow,
            FakeConstants.SeatsFromSeatNumber,
            FakeConstants.SeatsUntilSeatNumber,
            _cancellationToken)
            .Returns(_validSeatsList);
        _ticketsRepository.Add(Arg.Any<Ticket>(), _cancellationToken)
            .Returns(_validTicket);

        // Act
        var ticket = await _sut.Handle(command, _cancellationToken);

        // Assert
        ticket.Showtime.Id.Should().Be(_validShowtime.Id);
    }

    [Fact]
    public async Task Handle_ShouldReturnTicketWithRightCreatedAtDateTime_WhenValidCommandIsPassed()
    {
        // Arrange
        ReserveTicketCommand command = new(
            _validShowtime.Id,
            FakeConstants.SeatsRow,
            FakeConstants.SeatsFromSeatNumber,
            FakeConstants.SeatsUntilSeatNumber);

        _showtimesRepository.GetWithAuditoriumMovieAndTickets(_validShowtime.Id, _cancellationToken)
            .Returns(Task.FromResult<Showtime?>(_validShowtime));
        _seatsRepository.GetSeatsInRowWithAuditorium(
            _validShowtime.Auditorium.Id,
            FakeConstants.SeatsRow,
            FakeConstants.SeatsFromSeatNumber,
            FakeConstants.SeatsUntilSeatNumber,
            _cancellationToken)
            .Returns(_validSeatsList);
        _ticketsRepository.Add(Arg.Any<Ticket>(), _cancellationToken)
            .Returns(_validTicket);

        // Act
        var ticket = await _sut.Handle(command, _cancellationToken);

        // Assert
        ticket.CreatedAt.Should().Be(_validTicket.CreatedAt);
    }

    [Fact]
    public async Task Handle_ShouldReturnUnpaidTicket_WhenValidCommandIsPassed()
    {
        // Arrange
        ReserveTicketCommand command = new(
            _validShowtime.Id,
            FakeConstants.SeatsRow,
            FakeConstants.SeatsFromSeatNumber,
            FakeConstants.SeatsUntilSeatNumber);

        _showtimesRepository.GetWithAuditoriumMovieAndTickets(_validShowtime.Id, _cancellationToken)
            .Returns(Task.FromResult<Showtime?>(_validShowtime));
        _seatsRepository.GetSeatsInRowWithAuditorium(
            _validShowtime.Auditorium.Id,
            FakeConstants.SeatsRow,
            FakeConstants.SeatsFromSeatNumber,
            FakeConstants.SeatsUntilSeatNumber,
            _cancellationToken)
            .Returns(_validSeatsList);
        _ticketsRepository.Add(Arg.Any<Ticket>(), _cancellationToken)
            .Returns(_validTicket);

        // Act
        var ticket = await _sut.Handle(command, _cancellationToken);

        // Assert
        ticket.Paid.Should().BeFalse();
    }

    [Fact]
    public async Task Handle_ShouldReturnTicketWithSelectedSeats_WhenValidCommandIsPassed()
    {
        // Arrange
        ReserveTicketCommand command = new(
            _validShowtime.Id,
            FakeConstants.SeatsRow,
            FakeConstants.SeatsFromSeatNumber,
            FakeConstants.SeatsUntilSeatNumber);

        _showtimesRepository.GetWithAuditoriumMovieAndTickets(_validShowtime.Id, _cancellationToken)
            .Returns(Task.FromResult<Showtime?>(_validShowtime));
        _seatsRepository.GetSeatsInRowWithAuditorium(
            _validShowtime.Auditorium.Id,
            FakeConstants.SeatsRow,
            FakeConstants.SeatsFromSeatNumber,
            FakeConstants.SeatsUntilSeatNumber,
            _cancellationToken)
            .Returns(_validSeatsList);
        _ticketsRepository.Add(Arg.Any<Ticket>(), _cancellationToken)
            .Returns(_validTicket);

        // Act
        var ticket = await _sut.Handle(command, _cancellationToken);

        // Assert
        ticket.Seats.Should().BeEquivalentTo(_validSeatsList);
    }
}
