using Cinema.Domain.Auditorium;

namespace Cinema.UnitTests.Fakes;

internal static class AuditoriumFakes
{
    internal static Auditorium ValidAuditorium =>
        new(
            new(FakeConstants.AuditoriumId),
            FakeConstants.AuditoriumRows,
            FakeConstants.AuditoriumSeatsPerRow);
}
