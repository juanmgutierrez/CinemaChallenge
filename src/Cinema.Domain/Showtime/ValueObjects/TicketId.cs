using Cinema.Domain.Common.Models;
using Cinema.Domain.Showtime.Exceptions;

namespace Cinema.Domain.Showtime.ValueObjects;

public sealed class TicketId : EntityId<Guid>
{
    public TicketId(Guid value) : base(value)
    {
        if (value == Guid.Empty)
            throw new EmptyTicketIdException($"Ticket id cannot be empty");
    }
}
