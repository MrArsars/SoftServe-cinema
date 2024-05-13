using Core.Interfaces;

namespace Core.Models.Movie;

public class MovieDto : IEntity
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

    public MovieDto()
    {
    }

    public MovieDto(Movie movie)
    {
        Id = movie.Id;
        Name = movie.Name;
        ImageUrl = movie.ImageUrl;
        TrailerUrl = movie.TrailerUrl;
        AgeRestriction = movie.AgeRestriction;
        Duration = movie.Duration;
        Rating = movie.Rating;
        Description = movie.Description;
        ActorIds = movie.Actors.Select(a => a.Id).ToList();
        GenreIds = movie.Genres.Select(g => g.Id).ToList();
    }
}