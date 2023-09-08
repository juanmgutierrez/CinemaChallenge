namespace Cinema.UnitTests.Fakes;

internal static class FakeConstants
{
    // Auditorium
    internal static readonly Guid AuditoriumId = new("00000000-0000-0000-0000-000000000001");
    internal const int AuditoriumRows = 10;
    internal const int AuditoriumSeatsPerRow = 10;

    // Movie
    internal static readonly Guid MovieId = new("00000000-0000-0000-0000-000000000002");
    internal const string MovieImdbId = "tt1234567";

    // Seat
    internal const short SeatsRow = 1;
    internal const short SeatsFromSeatNumber = 2;
    internal const short SeatsUntilSeatNumber = 4;
}
