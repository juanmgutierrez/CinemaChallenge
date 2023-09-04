using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Common.Models;

namespace Cinema.Domain.Auditorium;

public sealed class Auditorium : AggregateRoot<AuditoriumId>
{
    public Auditorium(AuditoriumId id) : base(id)
    {
    }
}
