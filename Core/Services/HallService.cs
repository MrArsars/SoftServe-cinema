using BusinessLogic.Interfaces;
using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class HallService(IRepository<Hall> hallRepository) : IHallService
{
    public List<Hall> GetAll()
    {
        var res = hallRepository.GetAll().ToList();
        return res;
    }

    public Hall? GetById(Guid id)
    {
        var res = hallRepository.GetById(id);
        return res;
    }

    public Hall Update(Hall hall)
    {
        try
        {
            hallRepository.Update(hall);
            hallRepository.Save();
            return hall;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Hall Create(Hall hall)
    {
        try
        {
            hallRepository.Insert(hall);
            hallRepository.Save();
            return hall;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool Delete(Hall hall)
    {
        try
        {
            hallRepository.Delete(hall);
            hallRepository.Save();
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
            hallRepository.Delete(id);
            hallRepository.Save();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}