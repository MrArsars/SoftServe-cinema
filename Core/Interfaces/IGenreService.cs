using Core.Models;

namespace Core.Interfaces;

public interface IGenreService
{
    List<Genre> GetAll();
    Genre? GetById(Guid id);
    Genre Update(Genre actor);
    Genre Create(Genre genre);
    bool Delete(Genre genre);
    bool Delete(int id);
}