using AutoMapper;
using BusinessLogic.Interfaces;
using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class ActorService : IActorService
{
    private readonly IRepository<Actor> _actorRepository;

    public ActorService(IRepository<Actor> actorRepository)
    {
        this._actorRepository = actorRepository;
    }

    public List<Actor> GetAll()
    {
        var res = _actorRepository.GetAll().ToList();
        return res;
    }

    public Actor Create(Actor actor)
    {
        try
        {
            _actorRepository.Insert(actor);
            _actorRepository.Save();
            return actor;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}