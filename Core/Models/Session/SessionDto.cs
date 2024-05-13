namespace Core.Models;

public class SessionDto
{
    public Guid Id { get; set; }
    public Guid MovieId { get; set; } = new();
    public Guid HallId { get; set; } = new();
    public DateTime DateTimeStart { get; set; }
    public int Price { get; set; }
}