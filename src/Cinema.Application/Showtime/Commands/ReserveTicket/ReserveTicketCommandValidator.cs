using FluentValidation;

namespace Cinema.Application.Showtime.Commands.ReserveTicket;

public class ReserveTicketCommandValidator : AbstractValidator<ReserveTicketCommand>
{
    public ReserveTicketCommandValidator()
    {
        RuleFor(ticket => ticket.ShowtimeId).NotEmpty();
        RuleFor(ticket => ticket.ShowtimeId.Value).NotEmpty();
        RuleFor(ticket => ticket.ToSeatNumber).GreaterThanOrEqualTo(ticket => ticket.FromSeatNumber);
    }
}
