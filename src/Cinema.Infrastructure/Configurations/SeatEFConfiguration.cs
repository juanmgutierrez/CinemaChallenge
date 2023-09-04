using Cinema.Domain.Auditorium;
using Cinema.Domain.Auditorium.Entities;
using Cinema.Domain.Auditorium.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configurations;

internal sealed class SeatEFConfiguration : IEntityTypeConfiguration<Seat>
{
    public void Configure(EntityTypeBuilder<Seat> builder)
    {
        builder.HasKey(seat => seat.Id);
        builder.Property(seat => seat.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(id => id.Value, value => new SeatId(value));

        builder.Property(seat => seat.Row).IsRequired();
        builder.Property(seat => seat.SeatNumber).IsRequired();

        // TODO Add FK with Auditorium
        //public required Auditorium Auditorium { get; init; }
        builder.HasOne<Auditorium>()
            .WithMany()
            .HasForeignKey(seat => seat.AuditoriumId)
            .IsRequired();

        builder.HasIndex(seat => new { seat.AuditoriumId, seat.Row, seat.SeatNumber })
            .IsUnique();
    }
}
