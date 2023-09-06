using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.ValueObjects;
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
            .HasConversion(id => id.Value, value => new TicketId(value));

        builder.Property(ticket => ticket.CreatedAt).IsRequired();

        builder.HasOne(ticket => ticket.Showtime)
            .WithMany(showtime => showtime.Tickets)
            .HasForeignKey(ticket => ticket.ShowtimeId)
            .IsRequired();
        builder.HasMany(ticket => ticket.Seats)
            .WithMany();

        // TODO Document this index
        builder.HasIndex(ticket => new { ticket.Paid, ticket.CreatedAt }, "IX_Ticket_IsActive");
    }
}
