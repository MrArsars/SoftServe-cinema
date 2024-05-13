using AutoMapper;
using BusinessLogic.Interfaces;
using Core.Interfaces;
using Core.Models;
using Core.Models.Movie;
using Core.Models.Session;

namespace Core.Services;

public class SessionService : ISessionService
{
    private readonly IRepository<SessionDto> _sessionRepository;
    private readonly IRepository<Hall> _hallRepository;
    private readonly IRepository<MovieDto> _movieRepository;

    public SessionService(
        IRepository<SessionDto> sessionRepository, IRepository<Hall> hallRepository,
        IRepository<MovieDto> movieRepository)
    {
        _sessionRepository = sessionRepository;
        _hallRepository = hallRepository;
        _movieRepository = movieRepository;
    }

    public List<Session> GetAll()
    {
        var sessionDtos = _sessionRepository.GetAll().ToList();
        return sessionDtos.Select(FromDto).ToList();
    }

    public Session? GetById(Guid id)
    {
        var res = _sessionRepository.GetById(id);
        return res is null ? null : FromDto(res);
    }

    public Session Update(Session session)
    {
        try
        {
            _sessionRepository.Update(new SessionDto(session));
            _sessionRepository.Save();
            return session;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Session Create(Session session)
    {
        try
        {
            _sessionRepository.Insert(new SessionDto(session));
            _sessionRepository.Save();
            return session;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool Delete(Session session)
    {
        try
        {
            _sessionRepository.Delete(new SessionDto(session));
            _sessionRepository.Save();
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
            _sessionRepository.Delete(id);
            _sessionRepository.Save();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    private Session FromDto(SessionDto sessionDto)
    {
        var movieDto = _movieRepository.GetById(sessionDto.MovieId);
        var hall = _hallRepository.GetById(sessionDto.HallId);
        return new Session(sessionDto, new Movie(movieDto), hall);
    }
}