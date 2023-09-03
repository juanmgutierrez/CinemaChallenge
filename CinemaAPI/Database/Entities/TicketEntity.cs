namespace CinemaAPI.Database.Entities;

public class TicketEntity
{
    private TicketEntity(int showtimeId)
    {
        CreatedTime = DateTimeOffset.Now;
        Paid = false;
        ShowtimeId = showtimeId;
    }

    public Guid Id { get; }
    public DateTimeOffset CreatedTime { get; private set; }
    public bool Paid { get; private set; }

    public int ShowtimeId { get; private set; }
    public ShowtimeEntity Showtime { get; } = null!;

    public ICollection<SeatEntity> Seats { get; set; } = new List<SeatEntity>();

    public void Pay() => Paid = true;

    public static TicketEntity Create(int showtimeId, ICollection<SeatEntity> selectedSeats)
    {
        TicketEntity ticket = new(showtimeId);
        ticket.Seats = selectedSeats;
        return ticket;
    }
}
