namespace Core.Models;

public class MovieDto
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty; 
    public List<Guid> GenreIds { get; set; } = [];
    public string ImageUrl { get; set; } = string.Empty;
    public string TrailerUrl { get; set; } = string.Empty;
    public int AgeRestriction { get; set; }
    public int Duration { get; set; }
    public float Rating { get; set; }
    public List<Guid> ActorIds { get; set; } = [];
    public string Description { get; set; } = string.Empty;
}