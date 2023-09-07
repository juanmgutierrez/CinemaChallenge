using FluentValidation;

namespace Cinema.Application.Showtime.Commands.CreateShowtime;

public class CreateShowtimeCommandValidator : AbstractValidator<CreateShowtimeCommand>
{
    public CreateShowtimeCommandValidator()
    {
        RuleFor(c => c.AuditoriumId).NotEmpty();
        RuleFor(c => c.AuditoriumId.Value).GreaterThan(0);
        RuleFor(c => c.MovieImdbId).NotEmpty();
        RuleFor(c => c.SessionDate).GreaterThan(DateTimeOffset.Now);
    }
}
