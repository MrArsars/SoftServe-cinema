using Core.Interfaces;

namespace Core.Models;

public class Genre : IEntity
{
    public Guid Id { get; set; }
    public string Name { get; set; } = string.Empty;
}