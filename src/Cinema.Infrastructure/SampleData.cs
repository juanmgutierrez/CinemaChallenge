using Cinema.Domain.Auditorium;
using Cinema.Domain.Auditorium.Entities;
using Cinema.Domain.Auditorium.ValueObjects;
using Cinema.Domain.Showtime;
using Cinema.Domain.Showtime.Entities;
using Cinema.Domain.Showtime.ValueObjects;
using Cinema.Infrastructure.Contexts;

namespace Cinema.Infrastructure;

public class SampleData
{
    public static async void Initialize(CinemaDbContext dbContext)
    {
        var auditorium1 = new Auditorium(new AuditoriumId(new Guid("80641A31-8D55-4487-BB1A-08F20753E6A4")), rows: 5, seatsPerRow: 10);
        var auditorium2 = new Auditorium(new AuditoriumId(new Guid("80641A31-8D55-4487-BB1A-08F20753E6A5")), rows: 30, seatsPerRow: 20);
        var auditorium3 = new Auditorium(new AuditoriumId(new Guid("80641A31-8D55-4487-BB1A-08F20753E6A6")), rows: 15, seatsPerRow: 12);
        dbContext.Auditoriums.Add(auditorium1);
        dbContext.Auditoriums.Add(auditorium2);
        dbContext.Auditoriums.Add(auditorium3);

        dbContext.Seats.AddRange(GenerateSeats(auditorium1));
        dbContext.Seats.AddRange(GenerateSeats(auditorium2));
        dbContext.Seats.AddRange(GenerateSeats(auditorium3));

        var movie1 = Movie.Create(
                new MovieId(new Guid("80641A31-8A44-4487-BB1A-08F20753E5B5")),
                "Eternal Sunshine of the Spotless Mind",
                "Eternal Sunshine of the Spotless Mind");

        dbContext.Movies.Add(movie1);

        dbContext.Showtimes.Add(
            Showtime.Create(
                DateTimeOffset.UtcNow.AddDays(1),
                movie1,
                auditorium1));

        await dbContext.SaveChangesAsync();
    }

    private static List<Seat> GenerateSeats(Auditorium auditorium)
    {
        var seats = new List<Seat>();
        for (short row = 1; row <= auditorium.Rows; row++)
            for (short seatNumber = 1; seatNumber <= auditorium.SeatsPerRow; seatNumber++)
                seats.Add(Seat.Create(new SeatId(Guid.NewGuid()), auditorium, row, seatNumber));

        return seats;
    }
}
