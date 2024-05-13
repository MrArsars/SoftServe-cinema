using AutoMapper;
using BusinessLogic.Interfaces;
using Core.Interfaces;
using Core.Models;
using Core.Models.Movie;

namespace Core.Services;

public class MovieService : IMovieService
{
    private readonly IRepository<MovieDto> _movieRepository;
    private readonly IRepository<Actor> _actorRepository;
    private readonly IRepository<Genre> _genreRepository;

    public MovieService(
        IRepository<MovieDto> movieRepository,
        IRepository<Actor> actorRepository,
        IRepository<Genre> genreRepository)
    {
        _movieRepository = movieRepository;
        _actorRepository = actorRepository;
        _genreRepository = genreRepository;
    }

    public List<Movie> GetAll()
    {
        var movieDtos = _movieRepository.GetAll().ToList();
        return movieDtos.Select(FromDto).ToList();
    }

    public Movie? GetById(Guid id)
    {
        var res = _movieRepository.GetById(id);
        return res is null ? null : FromDto(res);
    }

    public Movie Update(Movie movie)
    {
        try
        {
            _movieRepository.Update(new MovieDto(movie));
            _movieRepository.Save();
            return movie;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Movie Create(Movie movie)
    {
        try
        {
            _movieRepository.Insert(new MovieDto(movie));
            _movieRepository.Save();
            return movie;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool Delete(Movie movie)
    {
        try
        {
            _movieRepository.Delete(new MovieDto(movie));
            _movieRepository.Save();
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
            _movieRepository.Delete(id);
            _movieRepository.Save();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private Movie FromDto(MovieDto movieDto)
    {
        var genres = _genreRepository.GetByIds(movieDto.GenreIds).ToList();
        var actors = _actorRepository.GetByIds(movieDto.ActorIds).ToList();
        return new Movie(movieDto, genres, actors);
    }
}