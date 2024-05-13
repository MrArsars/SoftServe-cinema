using Core.Interfaces;

namespace Core.Models.Ticket;

public class TicketDto : IEntity
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SessionId { get; set; }
    public Guid PlaceId { get; set; }
}