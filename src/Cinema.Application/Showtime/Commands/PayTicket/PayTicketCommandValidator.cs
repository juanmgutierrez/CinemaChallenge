using FluentValidation;

namespace Cinema.Application.Showtime.Commands.PayTicket;

public class PayTicketCommandValidator : AbstractValidator<PayTicketCommand>
{
    public PayTicketCommandValidator()
    {
        RuleFor(ticket => ticket.Id).NotEmpty();
    }
}
