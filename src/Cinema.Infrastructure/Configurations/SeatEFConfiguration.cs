﻿using Cinema.Domain.Auditorium.Entities;
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
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => new SeatId(value));

        builder.Property(seat => seat.Row).IsRequired();
        builder.Property(seat => seat.SeatNumber).IsRequired();

        builder.HasOne(seat => seat.Auditorium)
            .WithMany()
            .HasForeignKey(seat => seat.AuditoriumId);

        builder.HasIndex(seat => new { seat.AuditoriumId, seat.Row, seat.SeatNumber })
            .IsUnique();
    }
}
