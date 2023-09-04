using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.ValueObjects;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configurations;

internal sealed class MovieEFConfiguration : IEntityTypeConfiguration<Movie>
{
    public void Configure(EntityTypeBuilder<Movie> builder)
    {
        builder.HasKey(movie => movie.Id);

        builder.Property(movie => movie.Id)
            .HasConversion(id => id.Value, value => new MovieId(value));

        builder.Property(movie => movie.Title)
            .IsRequired()
            .HasMaxLength(128);
        builder.Property(movie => movie.FullTitle)
            .IsRequired()
            .HasMaxLength(128);

        builder.Property(movie => movie.ImdbId).HasMaxLength(32);
        builder.Property(movie => movie.Image).HasMaxLength(256);
        builder.Property(movie => movie.Crew).HasMaxLength(256);
    }
}
