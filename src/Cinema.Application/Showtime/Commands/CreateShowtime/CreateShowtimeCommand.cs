using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Showtime.ValueObjects;
using MediatR;

namespace Cinema.Application.Showtime.Commands.CreateShowtime;

public sealed record CreateShowtimeCommand(AuditoriumId AuditoriumId, DateTimeOffset SessionDate, MovieId? MovieId, string? MovieImdbId)
    : IRequest<Domain.Showtime.Showtime>;
