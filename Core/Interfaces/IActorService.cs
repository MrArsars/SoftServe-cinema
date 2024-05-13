using Core.Models;

namespace Core.Interfaces;

public interface IActorService
{
    List<Actor> GetAll();
    Actor Create(Actor actor);
}