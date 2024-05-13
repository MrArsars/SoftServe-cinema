using Core.Interfaces;

namespace Core.Models.Ticket;

public class Ticket : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SessionId { get; set; }
    public Place Place { get; set; } = new();
}