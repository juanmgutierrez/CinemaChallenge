using Cinema.Domain.Auditorium.Exceptions;
using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Auditorium.ValueObjects;

public sealed class AuditoriumId : EntityId<Guid>
{
    public AuditoriumId(Guid value) : base(value)
    {
        if (value == Guid.Empty)
            throw new EmptyAuditoriumIdException($"Auditorium id cannot be empty");
    }
}
