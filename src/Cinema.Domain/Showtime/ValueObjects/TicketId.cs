using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Showtime.ValueObjects;

public sealed class TicketId : EntityId<Guid>
{
    public TicketId(Guid value) : base(value)
    {
    }
}
