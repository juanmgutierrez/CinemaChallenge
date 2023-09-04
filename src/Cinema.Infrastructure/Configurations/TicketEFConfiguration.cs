using Cinema.Domain.Showtime.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Cinema.Infrastructure.Configurations;

internal sealed class TicketEFConfiguration : IEntityTypeConfiguration<Ticket>
{
    public void Configure(EntityTypeBuilder<Ticket> builder)
    {
        builder.HasKey(ticket => ticket.Id);
        builder.Property(ticket => ticket.Id)
            .ValueGeneratedOnAdd()
            .HasConversion(id => id.Value, value => new(value));

        builder.Property(ticket => ticket.CreatedAt).IsRequired();

        builder.HasOne(ticket => ticket.Showtime)
            .WithMany(showtime => showtime.Tickets)
            .HasForeignKey(ticket => ticket.ShowtimeId)
            .IsRequired();
        builder.HasMany(ticket => ticket.Seats)
            .WithMany();
    }
}
