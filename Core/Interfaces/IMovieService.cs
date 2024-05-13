using Core.Models;
using Core.Models.Movie;

namespace Core.Interfaces;

public interface IMovieService
{
    List<Movie> GetAll();
    Movie? GetById(int id);
    Movie Update(Movie movie);
    Movie Create(Movie movie);
    bool Delete(Movie movie);
    bool Delete(int id);
}