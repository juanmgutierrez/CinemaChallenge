using Cinema.Domain.Showtime;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configurations;

internal sealed class ShowtimeEFConfiguration : IEntityTypeConfiguration<Showtime>
{
    public void Configure(EntityTypeBuilder<Showtime> builder)
    {
        builder.HasKey(showtime => showtime.Id);
        builder.Property(showtime => showtime.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(id => id.Value, value => new(value));

        builder.Property(showtime => showtime.SessionDate).IsRequired();

        builder.HasOne(showtime => showtime.Movie)
            .WithMany()
            .HasForeignKey(showtime => showtime.MovieId)
            .IsRequired();
        builder.HasOne(showtime => showtime.Auditorium)
            .WithMany()
            .HasForeignKey(showtime => showtime.AuditoriumId)
            .IsRequired();
    }
}
