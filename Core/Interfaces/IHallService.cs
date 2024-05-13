using Core.Models;

namespace Core.Services;

public interface IHallService
{
    List<Hall> GetAll();
    Hall? GetById(Guid id);
    Hall Update(Hall hall);
    Hall Create(Hall hall);
    bool Delete(Hall hall);
    bool Delete(int id);
}