using Core.Models;

namespace Core.Services;

public interface IActorService
{
    List<Actor> GetAll();
    Actor Create(Actor actor);
}