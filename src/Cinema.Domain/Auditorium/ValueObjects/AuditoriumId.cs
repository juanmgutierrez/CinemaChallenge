using Cinema.Domain.Auditorium.Exceptions;
using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Auditorium.ValueObjects;

public sealed class AuditoriumId : EntityId<int>
{
    public AuditoriumId(int value) : base(value)
    {
        NonPositiveAuditoriumIdException.ThrowIfNonPositive(value);
    }
}
