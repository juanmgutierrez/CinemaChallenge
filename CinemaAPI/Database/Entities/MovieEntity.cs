namespace CinemaAPI.Database.Entities;

public class MovieEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public string? ImdbId { get; set; }
    public string? Stars { get; set; }
    public DateTime? ReleaseDate { get; set; }

    public ICollection<ShowtimeEntity> Showtimes { get; set; } = new List<ShowtimeEntity>();
}
