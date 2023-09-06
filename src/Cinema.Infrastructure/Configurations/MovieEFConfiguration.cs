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
            .HasConversion(id => id.Value, value => new MovieId(value))
            .HasColumnOrder(0);

        builder.Property(movie => movie.Title)
            .IsRequired()
            .HasMaxLength(128)
            .HasColumnOrder(1);
        builder.Property(movie => movie.FullTitle)
            .IsRequired()
            .HasMaxLength(128)
            .HasColumnOrder(2);

        builder.Property(movie => movie.ImdbId)
            .HasMaxLength(32)
            .HasColumnOrder(3);
        builder.Property(movie => movie.ImdbRating)
            .HasColumnOrder(4);
        builder.Property(movie => movie.ImdbRatingCount)
            .HasColumnOrder(5);
        builder.Property(movie => movie.Crew)
            .HasMaxLength(256)
            .HasColumnOrder(6);

        builder.Property(movie => movie.ReleaseYear)
            .HasColumnOrder(7);
        builder.Property(movie => movie.Image)
            .HasMaxLength(256)
            .HasColumnOrder(8);
        builder.Property(movie => movie.Rank)
            .HasColumnOrder(9);
        builder.Property(movie => movie.Stars)
            .HasColumnOrder(10);
    }
}
