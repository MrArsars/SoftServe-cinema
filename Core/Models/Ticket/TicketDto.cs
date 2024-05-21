using Core.Interfaces;

namespace Core.Models.Ticket;

public class TicketDto : IEntity
{
    public Guid Id { get; set; }
    public Guid SessionId { get; set; }
    public Guid PlaceId { get; set; }

    public TicketDto()
    {
    }

    public TicketDto(Ticket ticket)
    {
        Id = ticket.Id;
        SessionId = ticket.Session.Id;
        PlaceId = ticket.Place.Id;
    }
}