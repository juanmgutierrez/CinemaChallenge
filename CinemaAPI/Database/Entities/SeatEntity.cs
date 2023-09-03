namespace CinemaAPI.Database.Entities;

public class SeatEntity
{
    public int Id { get; set; }
    public required short Row { get; set; }
    public required short SeatNumber { get; set; }

    public int AuditoriumId { get; set; }
    public AuditoriumEntity Auditorium { get; set; } = null!;
}
