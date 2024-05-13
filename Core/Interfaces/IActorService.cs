using Core.Models;

namespace Core.Interfaces;

public interface IActorService
{
    List<Actor> GetAll();
    Actor? GetById(int id);
    Actor Update(Actor actor);
    Actor Create(Actor actor);
    bool Delete(Actor actor);
    bool Delete(int id);
}