using Core.Interfaces;

namespace Core.Models;

public class Actor : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
    public string Surname { get; set; } = string.Empty;
}