using Core.Interfaces;

namespace Core.Models.Session;

public class SessionDto : IEntity
{
    public Guid Id { get; set; }
    public Guid MovieId { get; set; } = new();
    public Guid HallId { get; set; } = new();
    public DateTime DateTimeStart { get; set; }
    public int Price { get; set; }
    public List<Guid> ReservedPlaces { get; set; }
    
    public SessionDto(){}

    public SessionDto(Session session)
    {
        Id = session.Id;
        MovieId = session.Movie.Id;
        HallId = session.Hall.Id;
        DateTimeStart = session.DateTimeStart;
        Price = session.Price;
        ReservedPlaces = session.ReservedPlaces;
    }
}