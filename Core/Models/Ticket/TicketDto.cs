namespace Core.Models;

public class TicketDto
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SessionId { get; set; }
    public Guid PlaceId { get; set; }
}