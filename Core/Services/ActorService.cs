using BusinessLogic.Interfaces;
using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class ActorService(IRepository<Actor> actorRepository) : IActorService
{
    public List<Actor> GetAll()
    {
        var res = actorRepository.GetAll().ToList();
        return res;
    }

    public Actor? GetById(Guid id)
    {
        var res = actorRepository.GetById(id);
        return res;
    }

    public Actor Update(Actor actor)
    {
        try
        {
            actorRepository.Update(actor);
            actorRepository.Save();
            return actor;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Actor Create(Actor actor)
    {
        try
        {
            actorRepository.Insert(actor);
            actorRepository.Save();
            return actor;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool Delete(Actor actor)
    {
        try
        {
            actorRepository.Delete(actor);
            actorRepository.Save();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    public bool Delete(int id)
    {
        try
        {
            actorRepository.Delete(id);
            actorRepository.Save();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}