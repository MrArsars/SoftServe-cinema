using BusinessLogic.Interfaces;
using Core.Interfaces;
using Core.Models;
using Core.Models.Movie;
using Core.Models.Session;
using Core.Models.Ticket;

namespace Core.Services;

public class TicketService : ITicketService
{
    private readonly IRepository<TicketDto> _tickeRepository;
    private readonly IRepository<SessionDto> _sessionRepository;
    private readonly IRepository<Place> _placesRepository;
    private readonly IRepository<Hall> _hallRepository;
    private readonly IRepository<MovieDto> _movieRepository;
    private readonly IRepository<TicketDto> _ticketRepository;

    public TicketService(IRepository<TicketDto> tickeRepository, IRepository<SessionDto> sessionRepository, IRepository<Place> placesRepository, IRepository<Hall> hallRepository, IRepository<MovieDto> movieRepository, IRepository<TicketDto> ticketRepository)
    {
        _tickeRepository = tickeRepository;
        _sessionRepository = sessionRepository;
        _placesRepository = placesRepository;
        _hallRepository = hallRepository;
        _movieRepository = movieRepository;
        _ticketRepository = ticketRepository;
    }
    
    public List<Ticket> GetAll()
    {
        var ticketDtos = _tickeRepository.GetAll().ToList();
        return ticketDtos.Select(FromDto).ToList();
    }

    public Ticket? GetById(Guid id)
    {
        var ticketDto = _tickeRepository.GetById(id);
        return FromDto(ticketDto);
    }

    public List<Ticket>? GetAllByUser(Guid userId)
    {
        var ticketDtos = _tickeRepository.GetAll().Where(x => x.UserId.Equals(userId)).ToList();
        return ticketDtos.Select(FromDto).ToList();
    }

    public List<Ticket>? GetAllBySessionId(Guid sessionId)
    {
        var ticketDtos = _tickeRepository.GetAll().Where(x => x.SessionId.Equals(sessionId)).ToList();
        return ticketDtos.Select(FromDto).ToList();
    }
    
    public List<Guid>? GetAllIdsBySessionId(Guid sessionId)
    {
        var res = _tickeRepository.GetAll().Where(x => x.SessionId.Equals(sessionId)).ToList();
        return res.Select(x => x.Id).ToList();
    }

    public Ticket Update(Ticket ticket)
    {
        try
        {
            _tickeRepository.Update(new TicketDto(ticket));
            _tickeRepository.Save();
            return ticket;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public Ticket Create(Ticket ticket)
    {
        try
        {   
            _tickeRepository.Insert(new TicketDto(ticket));
            _tickeRepository.Save();
            return ticket;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }

    public bool Delete(Ticket ticket)
    {
        try
        {
            _tickeRepository.Delete(new TicketDto(ticket));
            _tickeRepository.Save();
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
            _tickeRepository.Delete(id);
            _tickeRepository.Save();
            return true;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }
    }
    
    private Ticket FromDto(TicketDto ticketDto)
    {
        var sessionDto = _sessionRepository.GetById(ticketDto.SessionId);
        var session = FromDto(sessionDto);
        var place = _placesRepository.GetById(sessionDto.HallId);
        
        return new Ticket(ticketDto, session, place);
    }
    
    private Session FromDto(SessionDto sessionDto)
    {
        var movieDto = _movieRepository.GetById(sessionDto.MovieId);
        var hall = _hallRepository.GetById(sessionDto.HallId);
        var reservedPlaces = _ticketRepository.GetAll().Where(x => x.SessionId.Equals(sessionDto.Id)).Select(x => x.Id).ToList();
        
        return new Session(sessionDto, new Movie(movieDto), hall, reservedPlaces);
    }
}