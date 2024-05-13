using BusinessLogic.Interfaces;
using Core.Interfaces;
using Core.Models;

namespace Core.Services;

public class GenreService : IGenreService
{
    private readonly IRepository<Genre> _genreRepository;

    public GenreService(IRepository<Genre> genreRepository)
    {
        _genreRepository = genreRepository;
    }

    public List<Genre> GetAll()
    {
        var res = _genreRepository.GetAll().ToList();
        return res;
    }

    public Genre? GetById(Guid id)
    {
        var res = _genreRepository.GetById(id);
        return res;
    }

    public Genre Update(Genre actor)
    {
        try
        {
            _genreRepository.Update(actor);
            _genreRepository.Save();
            return actor;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Genre Create(Genre genre)
    {
        try
        {
            _genreRepository.Insert(genre);
            _genreRepository.Save();
            return genre;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool Delete(Genre genre)
    {
        try
        {
            _genreRepository.Delete(genre);
            _genreRepository.Save();
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
            _genreRepository.Delete(id);
            _genreRepository.Save();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
}