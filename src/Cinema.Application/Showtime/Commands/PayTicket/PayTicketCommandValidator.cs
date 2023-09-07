using FluentValidation;

namespace Cinema.Application.Showtime.Commands.PayTicket;

public class PayTicketCommandValidator : AbstractValidator<PayTicketCommand>
{
    public PayTicketCommandValidator()
    {
        RuleFor(c => c.Id).NotEmpty();
    }
}
