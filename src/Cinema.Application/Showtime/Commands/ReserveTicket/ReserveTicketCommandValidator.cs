using FluentValidation;

namespace Cinema.Application.Showtime.Commands.ReserveTicket;

public class ReserveTicketCommandValidator : AbstractValidator<ReserveTicketCommand>
{
    public ReserveTicketCommandValidator()
    {
        RuleFor(c => c.ShowtimeId).NotEmpty();
        RuleFor(c => c.ShowtimeId.Value).GreaterThan(0);
        RuleFor(c => c.ToSeatNumber).GreaterThanOrEqualTo(c => c.FromSeatNumber);
    }
}
