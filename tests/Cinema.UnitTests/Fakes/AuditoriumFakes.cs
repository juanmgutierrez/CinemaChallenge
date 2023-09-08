using Cinema.Domain.Auditorium;

namespace Cinema.UnitTests.Fakes;

internal static class AuditoriumFakes
{
    internal static Auditorium ValidAuditorium =>
        new(
            new(Guid.NewGuid()),
            Constants.AuditoriumRows,
            Constants.AuditoriumSeatsPerRow);
}
