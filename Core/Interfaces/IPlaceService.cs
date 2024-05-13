using Core.Models;

namespace Core.Services;

public interface IPlaceService
{
    List<Place> GetAll();
    Place? GetById(Guid id);
    Place Update(Place place);
    Place Create(Place place);
    bool Delete(Place place);
    bool Delete(int id);
}