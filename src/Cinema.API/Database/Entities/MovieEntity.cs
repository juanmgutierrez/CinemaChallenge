namespace CinemaAPI.Database.Entities;

public class MovieEntity
{
    public int Id { get; set; }
    public required string Title { get; set; }
    public required string FullTitle { get; set; }
    public string? ImdbRating { get; set; }
    public string? ImdbRatingCount { get; set; }
    public short? ReleaseYear { get; set; }
    public string? Image { get; set; }
    public string? Crew { get; set; }
    public string? Stars { get; set; }

    public ICollection<ShowtimeEntity> Showtimes { get; set; } = new List<ShowtimeEntity>();
}
