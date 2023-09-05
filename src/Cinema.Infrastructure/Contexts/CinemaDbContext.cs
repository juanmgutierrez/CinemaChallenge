using Cinema.Domain.Auditorium;
using Cinema.Domain.Auditorium.Entities;
using Cinema.Domain.Showtime;
using Cinema.Domain.Showtime.Entities;
using Cinema.Infrastructure.Configurations;
using Microsoft.EntityFrameworkCore;

namespace Cinema.Infrastructure.Contexts;

public class CinemaDbContext : DbContext
{
    public CinemaDbContext(DbContextOptions<CinemaDbContext> options) : base(options)
    {
    }

    public DbSet<Auditorium> Auditoriums { get; set; }
    public DbSet<Seat> Seats { get; set; }
    public DbSet<Showtime> Showtimes { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<Ticket> Ticket { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new AuditoriumEFConfiguration());
        modelBuilder.ApplyConfiguration(new SeatEFConfiguration());
        modelBuilder.ApplyConfiguration(new ShowtimeEFConfiguration());
        modelBuilder.ApplyConfiguration(new MovieEFConfiguration());
        modelBuilder.ApplyConfiguration(new TicketEFConfiguration());
    }
}
