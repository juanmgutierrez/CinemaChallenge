using Cinema.Domain.Auditorium;
using Cinema.Domain.Auditorium.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configurations;

internal sealed class AuditoriumEFConfiguration : IEntityTypeConfiguration<Auditorium>
{
    public void Configure(EntityTypeBuilder<Auditorium> builder)
    {
        builder.HasKey(auditorium => auditorium.Id);
        builder.Property(auditorium => auditorium.Id)
            .ValueGeneratedNever()
            .HasConversion(id => id.Value, value => new AuditoriumId(value));

        builder.Property(auditorium => auditorium.Rows).IsRequired();
        builder.Property(auditorium => auditorium.SeatsPerRow).IsRequired();
    }
}
