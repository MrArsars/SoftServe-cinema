using BusinessLogic.Interfaces;
using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class PlaceService(IRepository<Place> placeRepository) : IPlaceService
{
    public List<Place> GetAll()
    {
        var res = placeRepository.GetAll().ToList();
        return res;
    }

    public Place? GetById(Guid id)
    {
        var res = placeRepository.GetById(id);
        return res;
    }

    public Place Update(Place place)
    {
        try
        {
            placeRepository.Update(place);
            placeRepository.Save();
            return place;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Place Create(Place place)
    {
        try
        {
            placeRepository.Insert(place);
            placeRepository.Save();
            return place;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool Delete(Place place)
    {
        try
        {
            placeRepository.Delete(place);
            placeRepository.Save();
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
            placeRepository.Delete(id);
            placeRepository.Save();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}