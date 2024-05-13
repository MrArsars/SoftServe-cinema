using System.ComponentModel.DataAnnotations.Schema;

namespace Core.Models;

public class Ticket
{
    public Guid Id { get; set; }
    public Guid UserId { get; set; }
    public Guid SessionId { get; set; }
    public Place Place { get; set; } = new();
}