using Cinema.Application.Showtime.Commands.PayTicket;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.Exceptions;
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
    private readonly Ticket _validTicket;
    private readonly PayTicketCommand _command;
    private readonly CancellationToken _cancellationToken = CancellationToken.None;

    public PayTicketCommandHandlerTests()
    {
        _sut = new PayTicketCommandHandler(_ticketsRepository);

        var sessionDate = DateTimeOffset.UtcNow.AddDays(1);
        var validShowtime = ShowtimeFakes.ValidShowtime(sessionDate);
        var validSeatsList = SeatFakes.ValidSeatsList(
            FakeConstants.SeatsRow,
            FakeConstants.SeatsFromSeatNumber,
            FakeConstants.SeatsUntilSeatNumber);
        _validTicket = TicketFakes.ValidTicket(
            validShowtime,
            validSeatsList);

        _command = new PayTicketCommand(_validTicket.Id);
    }

    [Fact]
    public void Handle_ShouldThrowInexistentTicketException_WhenTicketIdDoesNotExist()
    {
        // Arrange
        _ticketsRepository.Get(_command.Id, _cancellationToken).ReturnsNull();

        // Act
        var act = async () => await _sut.Handle(_command, _cancellationToken);

        // Assert
        act.Should().ThrowAsync<InexistentTicketException>()
            .WithMessage($"Ticket with id {_command.Id.Value} was not found");
    }

    [Fact]
    public async Task Handle_ShouldMarkTicketAsPaid_WhenValidCommandIsPassed()
    {
        // Arrange
        _ticketsRepository.Get(_command.Id, _cancellationToken).Returns(Task.FromResult<Ticket?>(_validTicket));

        // Act
        await _sut.Handle(_command, _cancellationToken);

        // Assert
        _validTicket.Paid.Should().BeTrue();
    }
}
