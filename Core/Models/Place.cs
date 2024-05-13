namespace Core.Models;

public class Place
{
    public Guid Id { get; set; }
    public int RowNumber { get; set; }
    public int SeatNumber { get; set; }
    public bool IsReserved { get; set; }
    public Guid HallId { get; set; }
}
