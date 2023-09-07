using FluentValidation;

namespace Cinema.Application.Showtime.Commands.CreateShowtime;

public class CreateShowtimeCommandValidator : AbstractValidator<CreateShowtimeCommand>
{
    public CreateShowtimeCommandValidator()
    {
        RuleFor(command => command.AuditoriumId).NotEmpty();
        RuleFor(command => command.AuditoriumId.Value).NotEmpty();
        RuleFor(command => command.MovieId).NotEmpty().When(command => string.IsNullOrEmpty(command.MovieImdbId));
        RuleFor(command => command.MovieId!.Value).NotEmpty().When(command => command.MovieId is not null);
        RuleFor(command => command.MovieImdbId).NotEmpty().When(command => command.MovieId is null);
        RuleFor(command => command.SessionDate).GreaterThan(DateTimeOffset.Now);
    }
}
