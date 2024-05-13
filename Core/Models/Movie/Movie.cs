namespace Core.Models;

public class Movie
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; 
    public List<Genre> Genres { get; set; } = [];
    public string ImageUrl { get; set; } = string.Empty;
    public string TrailerUrl { get; set; } = string.Empty;
    public int AgeRestriction { get; set; }
    public int Duration { get; set; }
    public float Rating { get; set; }
    public List<Actor> Actors { get; set; } = [];
    public string Description { get; set; } = string.Empty;
}