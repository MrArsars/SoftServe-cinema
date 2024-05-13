namespace Core.Models;

public class Session
{
    public Guid Id { get; set; }
    public Movie.Movie Movie { get; set; } = new();
    public Hall Hall { get; set; } = new();
    public DateTime DateTimeStart { get; set; }
    public int Price { get; set; }
}