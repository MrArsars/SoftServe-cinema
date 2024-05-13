using Core.Interfaces;

namespace Core.Models.Movie;

public class Movie : IEntity
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

    public Movie() {}
    public Movie(MovieDto movieDto, List<Genre> genres, List<Actor> actors)
    {
        Id = movieDto.Id;
        Name = movieDto.Name;
        ImageUrl = movieDto.ImageUrl;
        TrailerUrl = movieDto.TrailerUrl;
        AgeRestriction = movieDto.AgeRestriction;
        Duration = movieDto.Duration;
        Rating = movieDto.Rating;
        Description = movieDto.Description;
        Genres = genres;
        Actors = actors;
    }
}