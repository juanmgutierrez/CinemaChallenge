namespace CinemaAPI.Database.Entities;

public class AuditoriumEntity
{
    public int Id { get; set; }

    public ICollection<ShowtimeEntity> Showtimes { get; set; } = new List<ShowtimeEntity>();
    public ICollection<SeatEntity> Seats { get; set; } = new List<SeatEntity>();
}
