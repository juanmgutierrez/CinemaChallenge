using CinemaAPI.Database.Entities;
using Microsoft.EntityFrameworkCore;

namespace CinemaAPI.Database;

public class CinemaContext : DbContext
{
    public CinemaContext(DbContextOptions<CinemaContext> options) : base(options)
    {

    }

    public DbSet<AuditoriumEntity> Auditoriums { get; set; }
    public DbSet<SeatEntity> Seats { get; set; }
    public DbSet<ShowtimeEntity> Showtimes { get; set; }
    public DbSet<MovieEntity> Movies { get; set; }
    public DbSet<TicketEntity> Tickets { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AuditoriumEntity>(build =>
        {
            build.HasKey(auditorium => auditorium.Id);
            build.Property(auditorium => auditorium.Id).ValueGeneratedOnAdd();
        });

        modelBuilder.Entity<SeatEntity>(build =>
        {
            build.HasKey(seat => seat.Id);
            //build.Property(seat => seat.Row).IsRequired();
            //build.Property(seat => seat.SeatNumber).IsRequired();
            build.HasIndex(seat => new { seat.AuditoriumId, seat.Row, seat.SeatNumber }).IsUnique();

            build.HasOne(seat => seat.Auditorium).WithMany(auditorium => auditorium.Seats).HasForeignKey(seat => seat.AuditoriumId).IsRequired();
        });

        modelBuilder.Entity<ShowtimeEntity>(build =>
        {
            build.HasKey(showtime => showtime.Id);
            build.Property(showtime => showtime.Id).ValueGeneratedOnAdd();
            //build.Property(showtime => showtime.SessionDate).IsRequired();

            build.HasOne(showtime => showtime.Movie).WithMany(movie => movie.Showtimes).HasForeignKey(showtime => showtime.MovieId);
            build.HasOne(showtime => showtime.Auditorium).WithMany(auditorium => auditorium.Showtimes).HasForeignKey(showtime => showtime.AuditoriumId).IsRequired();
        });

        modelBuilder.Entity<MovieEntity>(build =>
        {
            build.HasKey(movie => movie.Id);
            build.Property(movie => movie.Id).ValueGeneratedOnAdd();
            //build.Property(movie => movie.Title).IsRequired();
        });

        modelBuilder.Entity<TicketEntity>(build =>
        {
            build.HasKey(ticket => ticket.Id);
            build.Property(ticket => ticket.Id).ValueGeneratedOnAdd();
            build.Property(ticket => ticket.CreatedTime).IsRequired();
            build.Property(ticket => ticket.Paid).IsRequired();

            build.HasOne(ticket => ticket.Showtime).WithMany(showtime => showtime.Tickets).HasForeignKey(ticket => ticket.ShowtimeId).IsRequired();
            build.HasMany(ticket => ticket.Seats).WithMany();
        });
    }
}
