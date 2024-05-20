using Core.Interfaces;

namespace Core.Models;

public class Place : IEntity
{
    public Guid Id { get; set; }
    public int RowNumber { get; set; }
    public int SeatNumber { get; set; }
    public Guid HallId { get; set; }
}
