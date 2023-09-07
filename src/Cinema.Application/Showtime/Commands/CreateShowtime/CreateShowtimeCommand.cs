using Cinema.Domain.Auditorium.ValueObjects;
using MediatR;

namespace Cinema.Application.Showtime.Commands.CreateShowtime;

public sealed record CreateShowtimeCommand(AuditoriumId AuditoriumId, string MovieImdbId, DateTimeOffset SessionDate)
    : IRequest<Domain.Showtime.Showtime>;
