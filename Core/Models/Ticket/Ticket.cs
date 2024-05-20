using Core.Interfaces;

namespace Core.Models.Ticket;

public class Ticket : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Session.Session Session { get; set; } = new();
    public Place Place { get; set; } = new();

    public Ticket()
    {
    }

    public Ticket(TicketDto ticketDto, Session.Session session, Place place)
    {
        Id = ticketDto.Id;
        UserId = ticketDto.UserId;
        Session = session;
        Place = place;
    }
}