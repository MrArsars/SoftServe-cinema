using Core.Interfaces;
using Core.Models.Movie;

namespace Core.Models.Session;

public class Session : IEntity
{
    public Guid Id { get; set; }
    public Movie.Movie Movie { get; set; } = new();
    public Hall Hall { get; set; } = new();
    public DateTime DateTimeStart { get; set; }
    public int Price { get; set; }
    
    public Session() {}
    public Session(SessionDto sessionDto, Movie.Movie movie, Hall hall)
    {
        Id = sessionDto.Id;
        Movie = movie;
        Hall = hall;
        DateTimeStart = sessionDto.DateTimeStart;
        Price = sessionDto.Price;
    }
}