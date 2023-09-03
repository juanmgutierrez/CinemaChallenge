namespace CinemaAPI.Database.Entities;

public class ShowtimeEntity
{
    public int Id { get; set; }
    public DateTimeOffset SessionDate { get; set; }

    public int MovieId { get; set; }
    public MovieEntity Movie { get; set; } = null!;

    public int AuditoriumId { get; set; }
    public AuditoriumEntity Auditorium { get; set; } = null!;

    public ICollection<TicketEntity> Tickets { get; set; } = new List<TicketEntity>();
}
